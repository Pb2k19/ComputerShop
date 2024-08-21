using System.Security.Cryptography;
using System.Text;

namespace ComputerShop.Server.Cryptography.Hash;

public class PlainTextHashAlgorithm : IHashAlgorithm
{
    public string AlgorithmName => "PlainText";

    public (byte[] hash, byte[] salt) CreateHash(byte[] password)
    {
        return CreateHash(password, Array.Empty<byte>());
    }

    public (byte[] hash, byte[] salt) CreateHash(byte[] password, byte[] salt)
    {
        return (password, Array.Empty<byte>());
    }

    public string CreateHashString(byte[] password)
    {
        return Encoding.UTF8.GetString(password);
    }

    public bool VerifyHash(byte[] password, string hash)
    {
        return CryptographicOperations.FixedTimeEquals(password, Encoding.UTF8.GetBytes(hash));
    }
}
