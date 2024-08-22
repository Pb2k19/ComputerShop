using System.Text;

namespace ComputerShop.Server.Cryptography.Encryption;

public class PlainTextNoEncryption : IEncryption
{
    public byte[] Decrypt(string cipherText, byte[] key)
    {
        return Encoding.UTF8.GetBytes(cipherText);
    }

    public string Encrypt(byte[] plainText, byte[] key)
    {
        return Encoding.UTF8.GetString(plainText);
    }
}
