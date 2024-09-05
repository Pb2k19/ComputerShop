using ComputerShop.Server.Cryptography.Hash;
using System.Security.Cryptography;
using System.Text;

namespace ComputerShop.Server.Services.KeyService;

public class SymetricKeyService : IKeyService
{
    private readonly IHashAlgorithm hash;

    private readonly byte[] key;

    public string HashAlgorythmName => hash.AlgorithmName;

    public SymetricKeyService(IConfiguration configuration, IHashAlgorithm hash)
    {
        key = Encoding.UTF8.GetBytes(configuration["Settings:SymetricKey"]);
        this.hash = hash;
    }

    public SymetricKeyService(byte[] key, IHashAlgorithm hash)
    {
        this.key = key;
        this.hash = hash;
    }

    public (byte[] key, byte[] salt) GetEncryptionKey(int keyLength, string str)
    {
        return hash.KeyDerivation(CreateKeySource(str), keyLength);
    }

    public (byte[] key, byte[] salt) GetEncryptionKey(int keyLength, string str, byte[] salt)
    {
        return hash.KeyDerivation(CreateKeySource(str), salt, keyLength);
    }

    public (byte[] key, byte[] salt) GetDecryptionKey(int keyLength, string str)
    {
        return GetEncryptionKey(keyLength, str);
    }

    public (byte[] key, byte[] salt) GetDecryptionKey(int keyLength, string str, byte[] salt)
    {
        return GetEncryptionKey(keyLength, str, salt);
    }

    private byte[] CreateKeySource(string str)
    {
        byte[] strBytes = Encoding.UTF8.GetBytes(str);
        byte[] array = new byte[key.Length + strBytes.Length];

        strBytes.CopyTo(array, 0);
        key.CopyTo(array, strBytes.Length);

        return array;
    }

    public void Dispose()
    {
        CryptographicOperations.ZeroMemory(key);
    }
}
