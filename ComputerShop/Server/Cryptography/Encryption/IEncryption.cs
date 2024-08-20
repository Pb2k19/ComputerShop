namespace ComputerShop.Server.Cryptography.Encryption;

public interface IEncryption
{
    byte[] Decrypt(string cipherText, byte[] key);
    string Encrypt(byte[] plainText, byte[] key);
}
