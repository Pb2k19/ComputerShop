using ComputerShop.Server.Cryptography.Hash;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;

namespace ComputerShop.Server.Cryptography.Encryption;

public class AesGcmEncryptionService : IEncryption
{
    public const char Separator = '$';

    private readonly IHashAlgorithm hashAlgorithm;

    public AesGcmEncryptionService(IHashAlgorithm hashAlgorithm)
    {
        this.hashAlgorithm = hashAlgorithm;
    }

    public string Encrypt(byte[] plainText, byte[] key)
    {
        try
        {
            if (plainText is null || plainText.Length <= 0)
                throw new ArgumentException("CipherText is null or empty", nameof(plainText));
            if (key is null || key.Length != 32)
                throw new ArgumentException($"Key length is incorrect - {(key is not null ? key.Length : "null")}. Correct length is 32 (256bit)", nameof(key));

            using AesGcm aes = new(key);
            byte[] nonce = RandomNumberGenerator.GetBytes(AesGcm.NonceByteSizes.MaxSize);
            byte[] tag = new byte[AesGcm.TagByteSizes.MaxSize];
            byte[] cipherText = new byte[plainText.Length];
            (key, byte[] salt) = hashAlgorithm.CreateHash(key);

            aes.Encrypt(nonce, plainText, cipherText, tag);

            return $"AesGcm{Separator}{hashAlgorithm.AlgorithmName}{Separator}{Base64UrlEncoder.Encode(salt)}{Separator}{Base64UrlEncoder.Encode(nonce)}{Separator}{Base64UrlEncoder.Encode(tag)}{Separator}{Base64UrlEncoder.Encode(cipherText)}";

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

            (key, _) = hashAlgorithm.CreateHash(key, Base64UrlEncoder.DecodeBytes(splited[2]));

            using AesGcm aes = new(key);
            byte[] plainText = new byte[cipher.Length];
            byte[] nonce = Base64UrlEncoder.DecodeBytes(splited[3]);
            byte[] tag = Base64UrlEncoder.DecodeBytes(splited[4]);
            byte[] cipherBytes = Base64UrlEncoder.DecodeBytes(splited[5]);

            aes.Decrypt(nonce, cipherBytes, tag, plainText);

            return plainText;
        }
        finally
        {
            CryptographicOperations.ZeroMemory(key);
        }
    }
}
