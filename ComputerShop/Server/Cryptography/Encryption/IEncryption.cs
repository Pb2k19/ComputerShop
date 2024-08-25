namespace ComputerShop.Server.Cryptography.Encryption;

public interface IEncryption
{
    public int KeyLengthBytes { get; }
    byte[] Decrypt(string encrypted, byte[] key);
    string Encrypt(byte[] plainText, byte[] key);
}
