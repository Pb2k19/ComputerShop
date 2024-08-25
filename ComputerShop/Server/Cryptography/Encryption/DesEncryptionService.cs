using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;

namespace ComputerShop.Server.Cryptography.Encryption;

public class DesGcmEncryptionService : IEncryption
{
    public const char Separator = '$';
    public const int IvLength = 8;

    public int KeyLengthBytes { get; } = 192 / 8;

    public string Encrypt(byte[] plainText, byte[] key)
    {
        try
        {
            if (plainText is null || plainText.Length <= 0)
                throw new ArgumentException("Plain text is null or empty", nameof(plainText));
            if (key is null || key.Length != KeyLengthBytes)
                throw new ArgumentException($"Key length is incorrect - {(key is not null ? key.Length : "null")}. Correct length is 8 (64bit)", nameof(key));

            using DES aes = DES.Create();
            byte[] iv = RandomNumberGenerator.GetBytes(IvLength);
            byte[] cipherText = aes.EncryptCbc(plainText, iv);

            return $"DesCbc{Separator}{Base64UrlEncoder.Encode(iv)}{Separator}{Base64UrlEncoder.Encode(cipherText)}";
        }
        finally
        {
            CryptographicOperations.ZeroMemory(key);
        }
    }

    public byte[] Decrypt(string encrypted, byte[] key)
    {
        try
        {
            string[] splited = encrypted.Split(Separator);

            if (encrypted is null || encrypted.Length <= 0)
                throw new ArgumentException("Encrypted text is null or empty", nameof(encrypted));
            if (key is null || key.Length != KeyLengthBytes)
                throw new ArgumentException($"Key length is incorrect - {(key is not null ? key.Length : "null")}. Correct length is 8 (64bit)", nameof(key));


            using DES aes = DES.Create();
            byte[] iv = Base64UrlEncoder.DecodeBytes(splited[1]);
            byte[] cipherBytes = Base64UrlEncoder.DecodeBytes(splited[2]);

            return aes.DecryptCbc(iv, cipherBytes);
        }
        finally
        {
            CryptographicOperations.ZeroMemory(key);
        }
    }
}
