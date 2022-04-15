using ComputerShop.Shared.Models;
using ComputerShop.Shared.Models.User;
using Microsoft.IdentityModel.Tokens;
using System.Text.RegularExpressions;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using ComputerShop.Server.Models;
using ComputerShop.Server.Helpers;
using System.Security.Principal;

namespace ComputerShop.Server.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private AuthenticationHelper authentication = new();
        private readonly IConfiguration configuration;
        private List<User> users = new();

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
            var users = await GetAllUsers();
            email = email.ToLower();
            return users.FirstOrDefault(u => u.Email.ToLower().Equals(email));
        }
        public async Task<User?> GetUserById(string id)
        {
            var users = await GetAllUsers();
            return users.FirstOrDefault(u => u.Id.ToLower().Equals(id));
        }

        public async Task<ServiceResponse<Token>> Login(Login login)
        {
            if(login == null || string.IsNullOrWhiteSpace(login.Email) || string.IsNullOrWhiteSpace(login.Password))
            {
                return new ServiceResponse<Token> { Message = "Podane wartości nie mogą być puste", Success = false };
            }
            User? user = await GetUser(login.Email);
            if(user == null)
            {
                return new ServiceResponse<Token> { Message = "Adres email lub hasło jest nieprawidłowe", Success = false };
            }
            if(await authentication.VerifyHash(login.Password, user.Password))
            {
                Token token;
                try
                {
                    token = authentication.CreateToken(configuration, user);
                }
                catch
                {
                    return new ServiceResponse<Token> { Message = "Coś poszło nie tak - nie możemy Cię zalogować", Success = false };
                }
                return new ServiceResponse<Token> { Data = token };
            }
            else
            {
                return new ServiceResponse<Token> { Message = "Adres email lub hasło jest nieprawidłowe", Success = false };
            }
        }
        public async Task<SimpleServiceResponse> Register(Register register)
        {
            SimpleServiceResponse response = authentication.PasswordPolicyCheck(register);
            if (!response.Success)
            {
                return response;
            }
            if (await UserExists(register.Email))
            {
                return new SimpleServiceResponse
                {
                    Success = false,
                    Message = "Podany adres już istnieje w serwisie"
                };
            }
            User user = new()
            {
                Email = register.Email,
            };
            user.Password = await authentication.CreateHash(register.Password);
            if(!authentication.QuickHashCheck(user.Password))
            {
                return new SimpleServiceResponse
                {
                    Success = false,
                    Message = "Coś poszło nie tak"
                };
            }
            users.Add(user);
            return new SimpleServiceResponse();
        }
        public async Task<SimpleServiceResponse> ChangePassword(string userId, ChangePassword changePassword)
        {
            SimpleServiceResponse response = authentication.PasswordPolicyCheck(changePassword);
            if (!response.Success)
                return response;
            User? user = await GetUserById(userId);
            if (user == null)
                return new ServiceResponse<Token> { Message = "Coś poszło nie tak - nie można zmienić hasła", Success = false };
            List<Task> tasks = new();
            string newHash = string.Empty;
            bool verify = false;
            tasks.Add(Task.Run(async () => { verify = await authentication.VerifyHash(changePassword.CurrentPassword, user.Password); }));
            tasks.Add(Task.Run(async () => { newHash = await authentication.CreateHash(changePassword.Password); }));
            await Task.WhenAll(tasks);
            if(!verify)
                return new SimpleServiceResponse { Message = "Aktualne hasło jest nieprawidłowe", Success = false };
            if(!authentication.QuickHashCheck(newHash))
                return new ServiceResponse<Token> { Message = "Coś poszło nie tak - nie można zmienić hasła", Success = false };
            user.Password = newHash; //tmp zapis do bazy
            return new SimpleServiceResponse { Message = "Hasło zostało zmienione", Success = true};
        }
        public bool ValidateJWT(HttpRequest request, IIdentity? identity)
        {
            if(identity == null)
                return false;
            if (request.Cookies.TryGetValue("__Secure-Fgp", out string? cookie))
            {
                string? jwtHash = identity?.GetFingerprint();
                return authentication.ValidateFingerprint(cookie, jwtHash);
            }
            return false;
        }
    }
}