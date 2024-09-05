using ComputerShop.Server.Cryptography.DigitalSignature;
using ComputerShop.Server.Cryptography.Hash;
using ComputerShop.Server.DataAccess;
using ComputerShop.Server.Helpers;
using ComputerShop.Server.Models;
using ComputerShop.Shared.Models;
using ComputerShop.Shared.Models.User;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;

namespace ComputerShop.Server.Services.User
{
    public class UserService : IUserService
    {
        private readonly AuthenticationHelper authentication = new();
        private readonly IConfiguration configuration;
        private readonly IHttpContextAccessor contextAccessor;
        private readonly IUserData userData;
        private readonly IHashAlgorithm hashAlgorithm;
        private readonly IDigitalSignature digitalSignature;

        public UserService(IConfiguration configuration, IHttpContextAccessor contextAccessor, IUserData userData, IHashAlgorithm hashAlgorithm, IDigitalSignature digitalSignature)
        {
            this.configuration = configuration;
            this.contextAccessor = contextAccessor;
            this.userData = userData;
            this.hashAlgorithm = hashAlgorithm;
            this.digitalSignature = digitalSignature;
        }
        public async Task<List<UserModel>> GetAllUsersAsync()
        {
            return await userData.GetAllUsersAsync();
        }
        public async Task<bool> UserExists(string email)
        {
            email = email.ToLower();
            var users = await userData.GetAllUsersAsync();
            return users.Any(x => x.Email.ToLower().Equals(email) && x is RegisteredUser);
        }
        public async Task<RegisteredUser?> GetRegisteredUser(string email)
        {
            email = email.ToLower();
            return await userData.GetRegisteredUserByEmailAsync(email);
        }
        public async Task<UserModel?> GetUserByIdAsync(string id)
        {
            return await userData.GetUserByIdAsync(id);
        }
        public async Task<RegisteredUser?> GetRegisteredUserByIdAsync(string id)
        {
            return await userData.GetRegisteredUserByIdAsync(id);
        }
        public async Task<ServiceResponse<Token>> LoginAsync(Login login)
        {
            if (login == null || string.IsNullOrWhiteSpace(login.Email) || string.IsNullOrWhiteSpace(login.Password))
            {
                return new ServiceResponse<Token> { Message = "Podane wartości nie mogą być puste", Success = false };
            }
            RegisteredUser? user = await GetRegisteredUser(login.Email);
            if (user == null)
            {
                return new ServiceResponse<Token> { Message = "Adres email lub hasło jest nieprawidłowe", Success = false };
            }
            if (hashAlgorithm.VerifyPassword(Encoding.UTF8.GetBytes(login.Password), user.Password))
            {
                Token token;
                try
                {
                    token = authentication.CreateToken(digitalSignature, configuration, user);
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
        public async Task<SimpleServiceResponse> RegisterAsync(Register register)
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
            RegisteredUser user = new()
            {
                Email = register.Email,
                Password = hashAlgorithm.PasswordStorage(Encoding.UTF8.GetBytes(register.Password))
            };
            return await AddUserAsync(user);
        }
        public async Task<SimpleServiceResponse> ChangePasswordAsync(ChangePassword changePassword)
        {
            string? userId = GetUserId();
            if (userId == null)
                return new ServiceResponse<Token> { Message = "Coś poszło nie tak - nie można zmienić hasła", Success = false };
            SimpleServiceResponse response = authentication.PasswordPolicyCheck(changePassword);
            if (!response.Success)
                return response;
            RegisteredUser? user = await GetRegisteredUserByIdAsync(userId);
            if (user == null)
                return new ServiceResponse<Token> { Message = "Coś poszło nie tak - nie można zmienić hasła", Success = false };
            List<Task> tasks = new();
            string newHash = string.Empty;
            bool verify = false;
            tasks.Add(Task.Run(() => { verify = hashAlgorithm.VerifyPassword(Encoding.UTF8.GetBytes(changePassword.CurrentPassword), user.Password); }));
            tasks.Add(Task.Run(() => { newHash = hashAlgorithm.PasswordStorage(Encoding.UTF8.GetBytes(changePassword.Password)); }));
            await Task.WhenAll(tasks);
            if (!verify)
                return new SimpleServiceResponse { Message = "Aktualne hasło jest nieprawidłowe", Success = false };
            user.Password = newHash;
            await userData.UpdateUserAsync(user);
            return new SimpleServiceResponse { Message = "Hasło zostało zmienione", Success = true };
        }
        public async Task<SimpleServiceResponse> UpdateUserAsync(UserModel user)
        {
            try
            {
                await userData.UpdateUserAsync(user);
                return new SimpleServiceResponse { Success = true };
            }
            catch (MongoDB.Driver.MongoException ex)
            {
                return new SimpleServiceResponse { Success = false, Message = ex.Message };
            }
        }
        public async Task<SimpleServiceResponse> AddUserAsync(UserModel user)
        {
            try
            {
                await userData.CreateUser(user);
                return new SimpleServiceResponse { Success = true };
            }
            catch (MongoDB.Driver.MongoException ex)
            {
                return new SimpleServiceResponse { Success = false, Message = ex.Message };
            }
        }
        public SimpleServiceResponse ValidateJWT(HttpRequest request)
        {
            if (!Validate(request))
                return new SimpleServiceResponse { Message = "Nie można zweryfikować użytkownika", Success = false };
            else
                return new SimpleServiceResponse();
        }
        internal bool Validate(HttpRequest request)
        {
            IIdentity? identity = GetUserIdentity();
            if (identity == null)
                return false;
            if (request.Cookies.TryGetValue("__Secure-Fgp", out string? cookie))
            {
                string? jwtHash = identity?.GetFingerprint();
                return authentication.ValidateFingerprint(cookie, jwtHash);
            }
            return false;
        }
        public string? GetUserId()
        {
            return contextAccessor?.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
        }
        private IIdentity? GetUserIdentity()
        {
            return contextAccessor?.HttpContext?.User?.Identity;
        }
    }
}