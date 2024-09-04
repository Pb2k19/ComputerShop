using System.Security.Cryptography;
using System.Text;

namespace ComputerShop.Server.Services.KeyService;

public class AsymmetricKeyService : IKeyService
{
    private readonly byte[] pubKey, privKey;

    public string HashAlgorythmName => "None";

    public AsymmetricKeyService(IConfiguration configuration)
    {
        pubKey = Encoding.UTF8.GetBytes(configuration["Settings:PubKey"]);
        privKey = Encoding.UTF8.GetBytes(configuration["Settings:PrivKey"]);
    }

    public AsymmetricKeyService(byte[] pubKey, byte[] privKey)
    {
        this.pubKey = pubKey;
        this.privKey = privKey;
    }

    public (byte[] key, byte[] salt) GetDecryptionKey(int _, string str)
    {
        return (privKey, Array.Empty<byte>());
    }

    public (byte[] key, byte[] salt) GetDecryptionKey(int _, string str, byte[] salt)
    {
        return (privKey, Array.Empty<byte>());
    }

    public (byte[] key, byte[] salt) GetEncryptionKey(int _, string str)
    {
        return (pubKey, Array.Empty<byte>());
    }

    public (byte[] key, byte[] salt) GetEncryptionKey(int _, string str, byte[] salt)
    {
        return (pubKey, Array.Empty<byte>());
    }

    public void Dispose()
    {
        CryptographicOperations.ZeroMemory(pubKey);
        CryptographicOperations.ZeroMemory(privKey);
    }
}
