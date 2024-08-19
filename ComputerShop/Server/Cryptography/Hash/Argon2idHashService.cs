using Sodium;
using System.Security.Cryptography;
using System.Text;

namespace ComputerShop.Server.Cryptography.Hash
{
    public class Argon2idHashService : IHashService
    {
        public string AlgorithmName => "Argon2id";

        public byte[] CreateHash(byte[] password)
        {
            byte[] salt = RandomNumberGenerator.GetBytes(32);
            return PasswordHash.ArgonHashBinary(password, salt, PasswordHash.StrengthArgon.Interactive);
        }

        public string CreateHashString(byte[] password)
        {
            return PasswordHash.ArgonHashString(Encoding.UTF8.GetString(password), PasswordHash.StrengthArgon.Interactive);
        }

        public bool VerifyHash(byte[] password, string hash)
        {
            return PasswordHash.ArgonHashStringVerify(password, Encoding.UTF8.GetBytes(hash));
        }
    }
}
