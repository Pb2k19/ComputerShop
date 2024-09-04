using Sodium;
using System.Security.Cryptography;
using System.Text;

namespace ComputerShop.Server.Cryptography.Hash
{
    public class Argon2idHashAlgorithm : IHashAlgorithm
    {
        public const int DefaultSaltLength = 16, DefaultHashOutputLength = 16;

        public string AlgorithmName => "Argon2id";

        public (byte[] hash, byte[] salt) CreateHash(byte[] password)
        {
            return CreateHash(password, DefaultHashOutputLength);
        }

        public (byte[] hash, byte[] salt) CreateHash(byte[] password, int length)
        {
            byte[] salt = RandomNumberGenerator.GetBytes(DefaultSaltLength);
            return CreateHash(password, salt, length);
        }

        public (byte[] hash, byte[] salt) CreateHash(byte[] password, byte[] salt)
        {
            return CreateHash(password, salt, DefaultHashOutputLength);
        }

        public (byte[] hash, byte[] salt) CreateHash(byte[] password, byte[] salt, int length)
        {
            return (PasswordHash.ArgonHashBinary(password, salt, PasswordHash.StrengthArgon.Interactive, length, PasswordHash.ArgonAlgorithm.Argon_2ID13), salt);
        }

        public string CreateHashString(byte[] password)
        {
            return PasswordHash.ArgonHashString(Encoding.UTF8.GetString(password), PasswordHash.StrengthArgon.Interactive);
        }

        public bool VerifyHash(byte[] password, string hash)
        {
            return PasswordHash.ArgonHashStringVerify(Encoding.UTF8.GetBytes(hash), password);
        }
    }
}
