﻿using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;

namespace ComputerShop.Server.Cryptography.Encryption;

public class AesGcmEncryption : IEncryption
{
    public const char Separator = '$';

    public int KeyLengthBytes { get; } = 256 / 8;

    public string Encrypt(byte[] plainText, byte[] key)
    {
        try
        {
            if (plainText is null || plainText.Length <= 0)
                throw new ArgumentException("Plain text is null or empty", nameof(plainText));
            if (key is null || key.Length != KeyLengthBytes)
                throw new ArgumentException($"Key length is incorrect - {(key is not null ? key.Length : "null")}. Correct length is 32 (256bit)", nameof(key));

            using AesGcm aes = new(key);
            byte[] nonce = RandomNumberGenerator.GetBytes(AesGcm.NonceByteSizes.MaxSize);
            byte[] tag = new byte[AesGcm.TagByteSizes.MaxSize];
            byte[] cipherText = new byte[plainText.Length];

            aes.Encrypt(nonce, plainText, cipherText, tag);

            return $"AesGcm{Separator}{Base64UrlEncoder.Encode(nonce)}{Separator}{Base64UrlEncoder.Encode(tag)}{Separator}{Base64UrlEncoder.Encode(cipherText)}";
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
                throw new ArgumentException($"Key length is incorrect - {(key is not null ? key.Length : "null")}. Correct length is 32 (256bit)", nameof(key));

            using AesGcm aes = new(key);
            byte[] nonce = Base64UrlEncoder.DecodeBytes(splited[1]);
            byte[] tag = Base64UrlEncoder.DecodeBytes(splited[2]);
            byte[] cipherBytes = Base64UrlEncoder.DecodeBytes(splited[3]);
            byte[] plainText = new byte[cipherBytes.Length];

            aes.Decrypt(nonce, cipherBytes, tag, plainText);

            return plainText;
        }
        finally
        {
            CryptographicOperations.ZeroMemory(key);
        }
    }
}