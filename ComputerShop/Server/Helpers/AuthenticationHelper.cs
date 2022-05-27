using ComputerShop.Server.Models;
using ComputerShop.Shared.Models;
using ComputerShop.Shared.Models.User;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace ComputerShop.Server.Helpers
{
    public class AuthenticationHelper
    {
        private readonly RandomNumberGenerator numberGenerator = RandomNumberGenerator.Create();

        #region Fingerprint
        public Fingerprint CreateFingerprint()
        {
            Fingerprint fp = new();
            byte[] fingerprintBytes = new byte[50];
            numberGenerator.GetNonZeroBytes(fingerprintBytes);
            fp.FingerprintBase64 = Base64UrlEncoder.Encode(fingerprintBytes);
            fp.FingerprintHash = BCrypt.Net.BCrypt.HashPassword(fp.FingerprintBase64, 10);

            if (!fp.QuickFingerprintCheck())
                throw new CryptographicException("Fingerprint failed");

            return fp;
        }
        public bool ValidateFingerprint(Fingerprint fingerprint)
        {
            return ValidateFingerprint(fingerprint.FingerprintBase64, fingerprint.FingerprintHash);
        }
        public bool ValidateFingerprint(string? fingerprintBase64, string? fingerprintHash)
        {
            if (string.IsNullOrWhiteSpace(fingerprintBase64) || string.IsNullOrWhiteSpace(fingerprintHash))
                return false;
            return BCrypt.Net.BCrypt.Verify(fingerprintBase64, fingerprintHash);
        }
        #endregion

        #region Hash
        public async Task<string> CreateHash(string password)
        {
            string hash = string.Empty;
            await Task.Run(() => hash = BCrypt.Net.BCrypt.EnhancedHashPassword(password, 15, BCrypt.Net.HashType.SHA512));
            return hash;
        }
        public async Task<bool> VerifyHash(string password, string passwordFromDb)
        {
            bool result = false;
            await Task.Run(() => result = BCrypt.Net.BCrypt.EnhancedVerify(password, passwordFromDb, BCrypt.Net.HashType.SHA512));
            return result;
        }
        public bool QuickHashCheck(string hash)
        {
            if (string.IsNullOrEmpty(hash) || hash.Length < 60)
                return false;
            return true;
        }
        #endregion

        #region PasswordPolicy
        public SimpleServiceResponse PasswordPolicyCheck(Register register)
        {
            return PasswordPolicyCheck(register.Password, register.ConfPassword, register.Email);
        }
        public SimpleServiceResponse PasswordPolicyCheck(ChangePassword newPassword)
        {
            if (string.IsNullOrWhiteSpace(newPassword.Password))
                return new SimpleServiceResponse { Message = "Aktualne hasło jest wymagane", Success = false };
            return PasswordPolicyCheck(newPassword.Password, newPassword.ConfPassword);
        }
        public SimpleServiceResponse PasswordPolicyCheck(string? password, string? confPassword, string? email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return new SimpleServiceResponse { Message = "Pola formularza nie mogą być puste", Success = false };
            }
            var check = PasswordPolicyCheck(password, confPassword);
            if (!check.Success)
            {
                return check;
            }
            if (password?.ToLower().Contains(email.ToLower()) ?? true)
            {
                return new SimpleServiceResponse { Message = "Hasło nie może zawierać adresu email", Success = false };
            }
            return check;
        }
        public SimpleServiceResponse PasswordPolicyCheck(string? password, string? confPassword)
        {
            if (string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(confPassword))
            {
                return new SimpleServiceResponse { Message = "Hasła nie mogą być puste", Success = false };
            }
            else if (!confPassword.Equals(password))
            {
                return new SimpleServiceResponse { Message = "Hasła nie są identyczne", Success = false };
            }
            else if (!RegexPasswordTest(password))
            {
                return new SimpleServiceResponse { Message = "Hasło nie spełnia wymagań dotyczących złożoności", Success = false };
            }
            return new SimpleServiceResponse();
        }
        public bool RegexPasswordTest(string password)
        {
            Regex regex = new(@"(?=.*[A-Z])(?=.*[!@#$&*])(?=.*[0-9])(?=.*[a-z]).{8,30}");
            return regex.IsMatch(password);
        }
        #endregion

        #region Token
        public Token CreateToken(IConfiguration configuration, RegisteredUser user)
        {
            Token token = new();
            string confKey = configuration.GetSection("Settings:Token").Value;
            if (confKey == null || confKey.Length != 32)
            {
                return token;
            }

            Fingerprint fingerprint = CreateFingerprint();

            List<Claim> claims = new()
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim(CustomClaims.Fingerprint, fingerprint.FingerprintHash),
            };

            var eccPem = configuration["Settings:TokenPrivateEC"];

            var key = ECDsa.Create();
            key.ImportECPrivateKey(Convert.FromBase64String(eccPem), out _);

            SigningCredentials credentials = new(new ECDsaSecurityKey(key), SecurityAlgorithms.EcdsaSha256Signature);

            var jwtSecurityToken = new JwtSecurityToken(
                expires: DateTime.UtcNow.AddHours(24),
                claims: claims,
                signingCredentials: credentials);

            token.TokenValue = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);            
            token.SecureFgpBase64 = fingerprint.FingerprintBase64;

            if (!token.QuickTokenCheck())
                throw new CryptographicException("Token failed");

            return token;
        }
        #endregion
    }
}
