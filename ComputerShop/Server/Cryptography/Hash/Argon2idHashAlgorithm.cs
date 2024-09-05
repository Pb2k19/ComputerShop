using Sodium;
using System.Security.Cryptography;
using System.Text;

namespace ComputerShop.Server.Cryptography.Hash
{
    public class Argon2idHashAlgorithm : IHashAlgorithm
    {
        public const int DefaultSaltLength = 16, DefaultHashOutputLength = 32;

        public string AlgorithmName => "Argon2id";

        public (byte[] hash, byte[] salt) KeyDerivation(byte[] password)
        {
            return KeyDerivation(password, DefaultHashOutputLength);
        }

        public (byte[] hash, byte[] salt) KeyDerivation(byte[] password, int length)
        {
            byte[] salt = RandomNumberGenerator.GetBytes(DefaultSaltLength);
            return KeyDerivation(password, salt, length);
        }

        public (byte[] hash, byte[] salt) KeyDerivation(byte[] password, byte[] salt)
        {
            return KeyDerivation(password, salt, DefaultHashOutputLength);
        }

        public (byte[] hash, byte[] salt) KeyDerivation(byte[] password, byte[] salt, int length)
        {
            return (PasswordHash.ArgonHashBinary(password, salt, PasswordHash.StrengthArgon.Interactive, length, PasswordHash.ArgonAlgorithm.Argon_2ID13), salt);
        }

        public string PasswordStorage(byte[] password)
        {
            return PasswordHash.ArgonHashString(Encoding.UTF8.GetString(password), PasswordHash.StrengthArgon.Interactive);
        }

        public bool VerifyPassword(byte[] password, string hash)
        {
            return PasswordHash.ArgonHashStringVerify(Encoding.UTF8.GetBytes(hash), password);
        }
    }
}
