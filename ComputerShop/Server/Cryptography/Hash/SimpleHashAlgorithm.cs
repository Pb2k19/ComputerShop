using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;

namespace ComputerShop.Server.Cryptography.Hash;

public class SimpleHashAlgorithm : IHashAlgorithm
{
    public const string
        SHA2_256Name = "SHA-256",
        SHA2_384Name = "SHA-384",
        SHA2_512Name = "SHA-512",
        SHA1Name = "SHA-1",
        MD5Name = "MD5";

    public string AlgorithmName { get; private set; }

    public SimpleHashAlgorithm()
    {
        AlgorithmName = SHA2_512Name;
    }

    public SimpleHashAlgorithm(string algorithmName)
    {
        AlgorithmName = algorithmName;
    }

    public string CreateHashString(byte[] password)
    {
        return Base64UrlEncoder.Encode(CreateHash(password).hash);
    }

    public (byte[] hash, byte[] salt) CreateHash(byte[] password)
    {
        return CreateHash(password, Array.Empty<byte>());
    }

    public (byte[] hash, byte[] salt) CreateHash(byte[] password, byte[] salt)
    {
        using HashAlgorithm hashAlgorithm = GetAlgorithm(AlgorithmName);
        return (hashAlgorithm.ComputeHash(password), Array.Empty<byte>());
    }

    public (byte[] hash, byte[] salt) CreateHash(byte[] password, int length)
    {
        return CreateHash(password, Array.Empty<byte>(), length);
    }

    public (byte[] hash, byte[] salt) CreateHash(byte[] password, byte[] salt, int length)
    {
        int algorithmOutputLength = GetAlgorithmOutputBytesLength(AlgorithmName);

        if (algorithmOutputLength < length)
            throw new ArgumentException("Selected length is too long for selected algorithm", nameof(length));

        (byte[] result, salt) = CreateHash(password, Array.Empty<byte>(), length);

        return (result[..length], salt);
    }

    public bool VerifyHash(byte[] password, string hash)
    {
        byte[] hashBytes = Base64UrlEncoder.DecodeBytes(hash);

        return VerifyHash(password, hashBytes);
    }

    public bool VerifyHash(byte[] password, byte[] hash)
    {
        byte[] calculatedHash = CreateHash(password).hash;
        return CryptographicOperations.FixedTimeEquals(calculatedHash, hash);
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

    private static int GetAlgorithmOutputBytesLength(string hashName)
    {
        return hashName switch
        {
            SHA2_256Name => 256 / 8,
            SHA2_384Name => 384 / 8,
            SHA2_512Name => 512 / 8,
            SHA1Name => 160 / 8,
            MD5Name => 128 / 8,
            _ => throw new ArgumentException("Unsupported algorithm", nameof(hashName)),
        };
    }
}