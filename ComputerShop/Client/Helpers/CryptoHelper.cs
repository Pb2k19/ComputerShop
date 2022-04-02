namespace ComputerShop.Client.Helpers
{
    public class CryptoHelper
    {
        public async Task<string> CreateBcryptHashAsync(string password)
        {
            if(password == null)
                throw new ArgumentNullException(nameof(password));
            string hash = "";
            await Task.Run(() => hash = BCrypt.Net.BCrypt.EnhancedHashPassword(password, 10, BCrypt.Net.HashType.SHA512));
            if(string.IsNullOrWhiteSpace(hash))
                throw new System.Security.Cryptography.CryptographicException("Hash failed");
            return hash;
        }
    }
}
