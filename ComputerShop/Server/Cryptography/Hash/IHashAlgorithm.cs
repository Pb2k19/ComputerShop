namespace ComputerShop.Server.Cryptography.Hash;

public interface IHashAlgorithm
{
    public string AlgorithmName { get; }
    public string PasswordStorage(byte[] password);
    public (byte[] hash, byte[] salt) KeyDerivation(byte[] password);
    public (byte[] hash, byte[] salt) KeyDerivation(byte[] password, int length);
    public (byte[] hash, byte[] salt) KeyDerivation(byte[] password, byte[] salt);
    public (byte[] hash, byte[] salt) KeyDerivation(byte[] password, byte[] salt, int length);
    public bool VerifyPassword(byte[] password, string hash);
}