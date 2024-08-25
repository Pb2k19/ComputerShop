using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using System.Text;

namespace ComputerShop.Server.Cryptography.Encryption;

public class RsaAlgorithm : IEncryption
{
    public int KeyLengthBytes { get; } = 2048;

    public byte[] Decrypt(string encrypted, byte[] key)
    {
        using RSACryptoServiceProvider rsa = new();
        try
        {
            rsa.FromXmlString(Encoding.UTF8.GetString(key));

            return rsa.Decrypt(Base64UrlEncoder.DecodeBytes(encrypted), true);
        }
        finally
        {
            rsa.PersistKeyInCsp = false;
            CryptographicOperations.ZeroMemory(key);
        }
    }

    public string Encrypt(byte[] plainText, byte[] key)
    {
        using RSACryptoServiceProvider rsa = new();
        try
        {
            rsa.FromXmlString(Encoding.UTF8.GetString(key));

            byte[] encryptedData = rsa.Encrypt(plainText, true);

            return Base64UrlEncoder.Encode(encryptedData);
        }
        finally
        {
            rsa.PersistKeyInCsp = false;
            CryptographicOperations.ZeroMemory(key);
        }
    }
}
