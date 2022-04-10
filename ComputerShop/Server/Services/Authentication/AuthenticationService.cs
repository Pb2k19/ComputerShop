using ComputerShop.Shared.Models;
using ComputerShop.Shared.Models.User;
using Microsoft.IdentityModel.Tokens;
using System.Text.RegularExpressions;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;

namespace ComputerShop.Server.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private IConfiguration configuration;
        private List<User> users = new();
        private RandomNumberGenerator numberGenerator = RandomNumberGenerator.Create();

        public AuthenticationService(IConfiguration configuration)
        {
            users = new()
            {
                new User
                {
                    Id = "1",
                    Email = "user@example.com",
                    Password = "$2a$15$XYGv6r8KSy3eyUY.Is1yWuSYUGZ6kBH2o9nXfmkoMmw4W8dCcAUv6"
                }
            }; //1234@#aAbcd
            this.configuration = configuration;
        }

        public async Task<List<User>> GetAllUsers()
        {
            return users;
        }
        public async Task<bool> UserExists(string email)
        {
            var users = await GetAllUsers();
            email = email.ToLower();
            return users.Any(x => x.Email.ToLower().Equals(email));
        }
        public async Task<User?> GetUser(string email)
        {
            var user = await GetAllUsers();
            email = email.ToLower();
            return user.FirstOrDefault(u => u.Email.ToLower().Equals(email));
        }
        public async Task<ServiceResponse<(string, string)>> Login(Login login)
        {
            if(login == null || string.IsNullOrWhiteSpace(login.Email) || string.IsNullOrWhiteSpace(login.Password))
            {
                return new ServiceResponse<(string, string)> { Message = "Podane wartości nie mogą być puste", Success = false };
            }
            User? user = await GetUser(login.Email);
            if(user == null)
            {
                return new ServiceResponse<(string, string)> { Message = "Adres email lub hasło jest nieprawidłowe", Success = false };
            }
            if(await VerifyHash(login.Password, user.Password))
            {
                var token = await Task.Run(() => CreateToken(user));
                if(string.IsNullOrWhiteSpace(token.Item1) || string.IsNullOrWhiteSpace(token.Item2))
                {
                    return new ServiceResponse<(string, string)> { Message = "Coś poszło nie tak", Success = false };
                }
                return new ServiceResponse<(string, string)> { Data = token };
            }
            else
            {
                return new ServiceResponse<(string, string)> { Message = "Adres email lub hasło jest nieprawidłowe", Success = false };
            }
        }
        public async Task<ServiceResponse<string>> Register(Register register)
        {
            if (register == null || string.IsNullOrWhiteSpace(register.Email) || string.IsNullOrWhiteSpace(register.Password) 
                || string.IsNullOrWhiteSpace(register.ConfPassword))
            {
                return new ServiceResponse<string> { Message = "Podane wartości nie mogą być puste", Success = false };
            }
            if (!PasswordPolicyCheck(register))
            {
                return new ServiceResponse<string>
                {
                    Success = false,
                    Message = "Podane hasło nie spełnia wymagań"
                };
            }
            if (await UserExists(register.Email))
            {
                return new ServiceResponse<string>
                {
                    Success = false,
                    Message = "Podany adres już istnieje w serwisie"
                };
            }
            User user = new()
            {
                Email = register.Email,
            };
            user.Password = await CreateHash(register.Password);
            if(string.IsNullOrEmpty(user.Password) || user.Password.Length < 12)
            {
                return new ServiceResponse<string>
                {
                    Success = false,
                    Message = "Coś poszło nie tak"
                };
            }
            users.Add(user);
            return new ServiceResponse<string> { Data = user.Id };
        }
        protected async Task<string> CreateHash(string password)
        {
            string hash = string.Empty;
            await Task.Run(() => hash = BCrypt.Net.BCrypt.EnhancedHashPassword(password, 15, BCrypt.Net.HashType.SHA512));
            return hash;
        }
        protected async Task<bool> VerifyHash(string password, string passwordFromDb)
        {
            bool result = false;
            await Task.Run(() => result = BCrypt.Net.BCrypt.EnhancedVerify(password, passwordFromDb, BCrypt.Net.HashType.SHA512));
            return result;
        }
        protected (string, string) CreateToken(User user)
        {
            string confKey = configuration.GetSection("Settings:Token").Value;
            if(confKey == null || confKey.Length != 32)
            {
                return (string.Empty, string.Empty);
            }

            byte[] randomFingerPrint = new byte[64];
            numberGenerator.GetNonZeroBytes(randomFingerPrint);
            using SHA256 sha256 = SHA256.Create();
            byte[] fingerPringSha = sha256.ComputeHash(randomFingerPrint);
            string fingerPrintShaBase64 = Base64UrlEncoder.Encode(randomFingerPrint);

            List<Claim> claims = new()
            {                
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim("userFingerprint", fingerPrintShaBase64)
            };

            var eccPem = configuration["Settings:TokenPrivateEC"];

            var key = ECDsa.Create();
            key.ImportECPrivateKey(Convert.FromBase64String(eccPem), out _);

            SigningCredentials credentials = new(new ECDsaSecurityKey(key), SecurityAlgorithms.EcdsaSha256Signature);

            var token = new JwtSecurityToken(
                expires: DateTime.UtcNow.AddHours(24), 
                claims: claims, 
                signingCredentials: credentials);

            return (new JwtSecurityTokenHandler().WriteToken(token), Base64UrlEncoder.Encode(randomFingerPrint));
        }
        public virtual bool PasswordPolicyCheck(Register register)
        {
            if(register == null || string.IsNullOrWhiteSpace(register.Password) || 
               !register.ConfPassword.Equals(register.Password) || 
               register.Password.ToLower().Contains(register.Email.ToLower()))
            {
                return false;
            }
            Regex regex = new(@"(?=.*[A-Z])(?=.*[!@#$&*])(?=.*[0-9])(?=.*[a-z]).{8,30}");
            return regex.IsMatch(register.Password);
        }
    }
}
