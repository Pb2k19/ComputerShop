using ComputerShop.Server.Cryptography.Hash;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;

namespace ComputerShop.Server.Cryptography.Encryption;

public class DesGcmEncryptionService : IEncryption
{
    public const char Separator = '$';
    public const int IvLength = 8, KeyLengthBytes = 192 / 8;

    private readonly IHashAlgorithm hashService;

    public DesGcmEncryptionService(IHashAlgorithm hashService)
    {
        this.hashService = hashService;
    }

    public string Encrypt(byte[] plainText, byte[] key)
    {
        try
        {
            (key, byte[] salt) = hashService.CreateHash(key, KeyLengthBytes);

            if (plainText is null || plainText.Length <= 0)
                throw new ArgumentException("CipherText is null or empty", nameof(plainText));
            if (key is null || key.Length != KeyLengthBytes)
                throw new ArgumentException($"Key length is incorrect - {(key is not null ? key.Length : "null")}. Correct length is 8 (64bit)", nameof(key));

            using DES aes = DES.Create();
            byte[] iv = RandomNumberGenerator.GetBytes(IvLength);
            byte[] cipherText = aes.EncryptCbc(plainText, iv);

            return $"DesCbc{Separator}{hashService.AlgorithmName}{Separator}{Base64UrlEncoder.Encode(salt)}{Separator}{Base64UrlEncoder.Encode(iv)}{Separator}{Base64UrlEncoder.Encode(cipherText)}";
        }
        finally
        {
            CryptographicOperations.ZeroMemory(key);
        }
    }

    public byte[] Decrypt(string cipher, byte[] key)
    {
        try
        {
            string[] splited = cipher.Split(Separator);

            (key, _) = hashService.CreateHash(key, Base64UrlEncoder.DecodeBytes(splited[2]), KeyLengthBytes);

            using DES aes = DES.Create();
            byte[] iv = Base64UrlEncoder.DecodeBytes(splited[3]);
            byte[] cipherBytes = Base64UrlEncoder.DecodeBytes(splited[4]);

            return aes.DecryptCbc(iv, cipherBytes);
        }
        finally
        {
            CryptographicOperations.ZeroMemory(key);
        }
    }
}
