using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;

namespace ComputerShop.Server.Cryptography.Hash;

public class PBKDF2HashAlgorithm : IHashAlgorithm
{
    public const int NumberOfIterations = 210_000, SaltLength = 32, ResultLength = 64;
    public const char Separator = '$';

    public string AlgorithmName => "PBKDF2";

    public string CreateHashString(byte[] password)
    {
        byte[] salt = RandomNumberGenerator.GetBytes(SaltLength);
        byte[] result = CreateHashCore(password, salt);

        return $"PBKDF2{Separator}{NumberOfIterations}{Separator}{Base64UrlEncoder.Encode(salt)}{Separator}{Base64UrlEncoder.Encode(result)}";
    }

    public (byte[] hash, byte[] salt) CreateHash(byte[] password)
    {
        byte[] salt = RandomNumberGenerator.GetBytes(SaltLength);
        return CreateHash(password, salt);
    }

    public (byte[] hash, byte[] salt) CreateHash(byte[] password, byte[] salt)
    {
        return (CreateHashCore(password, salt), salt);
    }

    public byte[] CreateHashCore(byte[] password, byte[] salt)
    {
        using Rfc2898DeriveBytes hash = new(password, salt, NumberOfIterations, HashAlgorithmName.SHA512);
        return hash.GetBytes(ResultLength);
    }

    public bool VerifyHash(byte[] password, string hash)
    {
        string[] hashParts = hash.Split(Separator);
        byte[] calculatedHash = CreateHashCore(password, Base64UrlEncoder.DecodeBytes(hashParts[2]));

        return CryptographicOperations.FixedTimeEquals(calculatedHash, Base64UrlEncoder.DecodeBytes(hashParts[3]));
    }
}
