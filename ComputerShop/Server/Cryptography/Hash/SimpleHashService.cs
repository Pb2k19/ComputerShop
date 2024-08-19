using System.Security.Cryptography;

namespace ComputerShop.Server.Cryptography.Hash;

public class SimpleHashService : IHashService
{
    public const string
        SHA2_256Name = "SHA-256",
        SHA2_384Name = "SHA-384",
        SHA2_512Name = "SHA-512",
        SHA1Name = "SHA-1",
        MD5Name = "MD5";

    public string AlgorithmName { get; private set; }

    public SimpleHashService()
    {
        AlgorithmName = SHA2_512Name;
    }

    public SimpleHashService(string algorithmName)
    {
        AlgorithmName = algorithmName;
    }

    public byte[] CreateHash(byte[] password)
    {
        using HashAlgorithm hashAlgorithm = GetAlgorithm(AlgorithmName);
        return hashAlgorithm.ComputeHash(password);
    }

    public bool VerifyHash(byte[] password, byte[] hash)
    {
        return CryptographicOperations.FixedTimeEquals(password, hash);
    }

    private static HashAlgorithm GetAlgorithm(string hashName)
    {
        return hashName switch
        {
            SHA2_256Name => SHA256.Create(),
            SHA2_384Name => SHA384.Create(),
            SHA2_512Name => SHA512.Create(),
            SHA1Name => SHA1.Create(),
            MD5Name => MD5.Create(),
            _ => throw new ArgumentException("Unsupported algorithm", nameof(hashName)),
        };
    }
}