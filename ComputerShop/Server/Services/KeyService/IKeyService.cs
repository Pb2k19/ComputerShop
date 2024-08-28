namespace ComputerShop.Server.Services.KeyService;

public interface IKeyService : IDisposable
{
    string HashAlgorythmName { get; }
    (byte[] key, byte[] salt) GetDecryptionKey(int keyLength, string str);
    (byte[] key, byte[] salt) GetDecryptionKey(int keyLength, string str, byte[] salt);
    (byte[] key, byte[] salt) GetEncryptionKey(int keyLength, string str);
    (byte[] key, byte[] salt) GetEncryptionKey(int keyLength, string str, byte[] salt);
}
