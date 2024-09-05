using System.Security.Cryptography;
using System.Text;

namespace ComputerShop.Server.Cryptography.Hash;

public class PlainTextNoHash : IHashAlgorithm
{
    public string AlgorithmName => "PlainText";

    public (byte[] hash, byte[] salt) KeyDerivation(byte[] password)
    {
        return KeyDerivation(password, Array.Empty<byte>());
    }

    public (byte[] hash, byte[] salt) KeyDerivation(byte[] password, byte[] salt)
    {
        return (password, Array.Empty<byte>());
    }

    public (byte[] hash, byte[] salt) KeyDerivation(byte[] password, int length)
    {
        return KeyDerivation(password, Array.Empty<byte>(), length);
    }

    public (byte[] hash, byte[] salt) KeyDerivation(byte[] password, byte[] salt, int length)
    {
        if (password.Length < length)
        {
            byte[] passwordWithPadding = new byte[length];
            password.CopyTo(passwordWithPadding, 0);

            for (int i = password.Length; i < passwordWithPadding.Length; i++)
            {
                passwordWithPadding[i] = 61; // 61 -> =
            }

            return (passwordWithPadding[..length], Array.Empty<byte>());
        }

        return (password[..length], Array.Empty<byte>());
    }

    public string PasswordStorage(byte[] password)
    {
        return Encoding.UTF8.GetString(password);
    }

    public bool VerifyPassword(byte[] password, string hash)
    {
        return CryptographicOperations.FixedTimeEquals(password, Encoding.UTF8.GetBytes(hash));
    }
}
