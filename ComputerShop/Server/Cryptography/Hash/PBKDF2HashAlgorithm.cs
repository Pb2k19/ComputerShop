using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;

namespace ComputerShop.Server.Cryptography.Hash;

public class PBKDF2HashAlgorithm : IHashAlgorithm
{
    public const int NumberOfIterations = 600_000, DefaultSaltLength = 16, DefaultResultLength = 32;
    public const char Separator = '$';

    public string AlgorithmName => "PBKDF2";

    public string PasswordStorage(byte[] password)
    {
        byte[] salt = RandomNumberGenerator.GetBytes(DefaultSaltLength);
        byte[] result = KeyDerivationCore(password, salt);

        return $"PBKDF2{Separator}{NumberOfIterations}{Separator}{Base64UrlEncoder.Encode(salt)}{Separator}{Base64UrlEncoder.Encode(result)}";
    }

    public (byte[] hash, byte[] salt) KeyDerivation(byte[] password)
    {
        return KeyDerivation(password, DefaultResultLength);
    }

    public (byte[] hash, byte[] salt) KeyDerivation(byte[] password, int length)
    {
        byte[] salt = RandomNumberGenerator.GetBytes(DefaultSaltLength);
        return KeyDerivation(password, salt, length);
    }

    public (byte[] hash, byte[] salt) KeyDerivation(byte[] password, byte[] salt)
    {
        return KeyDerivation(password, salt, DefaultResultLength);
    }

    public (byte[] hash, byte[] salt) KeyDerivation(byte[] password, byte[] salt, int length)
    {
        return (KeyDerivationCore(password, salt, length), salt);
    }

    public byte[] KeyDerivationCore(byte[] password, byte[] salt, int length = DefaultResultLength)
    {
        using Rfc2898DeriveBytes hash = new(password, salt, NumberOfIterations, HashAlgorithmName.SHA256);
        return hash.GetBytes(length);
    }

    public bool VerifyPassword(byte[] password, string hash)
    {
        string[] hashParts = hash.Split(Separator);
        byte[] calculatedHash = KeyDerivationCore(password, Base64UrlEncoder.DecodeBytes(hashParts[2]));

        return CryptographicOperations.FixedTimeEquals(calculatedHash, Base64UrlEncoder.DecodeBytes(hashParts[3]));
    }
}
