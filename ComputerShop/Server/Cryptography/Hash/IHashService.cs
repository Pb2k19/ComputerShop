namespace ComputerShop.Server.Cryptography.Hash;

public interface IHashService
{
    public string AlgorithmName { get; }
    public string CreateHashString(byte[] password);
    public byte[] CreateHash(byte[] password);
    public bool VerifyHash(byte[] password, string hash);
}