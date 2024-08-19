namespace ComputerShop.Server.Cryptography.Hash;

public interface IHashService
{
    public string AlgorithmName { get; }
    public byte[] CreateHash(byte[] password);
    public bool VerifyHash(byte[] password, byte[] hash);
}