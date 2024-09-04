using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using System.Text;

namespace ComputerShop.Server.Cryptography.Encryption;

public class RsaAlgorithm : IEncryption
{
    public int KeyLengthBytes { get; } = 2048 / 8;

    public byte[] Decrypt(string encrypted, byte[] key)
    {
        using RSACryptoServiceProvider rsa = new();
        rsa.ImportFromPem(Encoding.UTF8.GetChars(key));

        return rsa.Decrypt(Base64UrlEncoder.DecodeBytes(encrypted), true);
    }

    public string Encrypt(byte[] plainText, byte[] key)
    {
        using RSACryptoServiceProvider rsa = new();
        rsa.ImportFromPem(Encoding.UTF8.GetChars(key));

        byte[] encryptedData = rsa.Encrypt(plainText, true);

        return Base64UrlEncoder.Encode(encryptedData);
    }
}
