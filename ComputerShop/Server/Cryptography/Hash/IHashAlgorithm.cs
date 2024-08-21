namespace ComputerShop.Server.Cryptography.Hash;

public interface IHashAlgorithm
{
    public string AlgorithmName { get; }
    public string CreateHashString(byte[] password);
    public (byte[] hash, byte[] salt) CreateHash(byte[] password);
    public (byte[] hash, byte[] salt) CreateHash(byte[] password, byte[] salt);
    public bool VerifyHash(byte[] password, string hash);
}