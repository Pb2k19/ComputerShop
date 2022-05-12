using ComputerShop.Shared.Models;
using ComputerShop.Shared.Models.User;
using ComputerShop.Server.Models;
using ComputerShop.Server.Helpers;
using System.Security.Principal;
using System.Security.Claims;

namespace ComputerShop.Server.Services.User
{
    public class UserService : IUserService
    {
        private AuthenticationHelper authentication = new();
        private readonly IConfiguration configuration;
        private readonly IHttpContextAccessor contextAccessor;
        private List<UserModel> users = new();

        public UserService(IConfiguration configuration, IHttpContextAccessor contextAccessor)
        {
            users = new()
            {
                new UserModel
                {
                    Id = "1",
                    Email = "user@example.com",
                    Password = "$2a$15$XYGv6r8KSy3eyUY.Is1yWuSYUGZ6kBH2o9nXfmkoMmw4W8dCcAUv6",
                    WishList = new(new List<WishListItem> {
                        new WishListItem { ProductId = "1" },
                        new WishListItem { ProductId = "2" } }),
                    Orders = new()
                    {
                        new OrderModel
                        {
                            CartItems = new()
                            {
                                new CartItem { Price = 200, ProductId = "1", Quantity = 20},
                                new CartItem { Price = 200, ProductId = "2", Quantity = 5},
                                new CartItem { Price = 200, ProductId = "3", Quantity = 10},
                            },
                            Id = "112132w1",
                            State = "Completed",
                            Total = 600
                        },
                        new OrderModel
                        {
                            CartItems = new()
                            {
                                new CartItem { Price = 200, ProductId = "1", Quantity = 20},
                            },
                            Id = "1213232",
                            State = "In Progres",
                            Total = 200
                        }
                    }
                    },
            }; //1234@#aAbcd
            this.configuration = configuration;
            this.contextAccessor = contextAccessor;
        }

        public async Task<List<UserModel>> GetAllUsers()
        {
            return users;
        }
        public async Task<bool> UserExists(string email)
        {
            var users = await GetAllUsers();
            email = email.ToLower();
            return users.Any(x => x.Email.ToLower().Equals(email));
        }
        public async Task<UserModel?> GetUser(string email)
        {
            var users = await GetAllUsers();
            email = email.ToLower();
            return users.FirstOrDefault(u => u.Email.ToLower().Equals(email));
        }
        public async Task<UserModel?> GetUserByIdAsync(string id)
        {
            var users = await GetAllUsers();
            return users.FirstOrDefault(u => u.Id.ToLower().Equals(id));
        }
        public async Task<ServiceResponse<Token>> LoginAsync(Login login)
        {
            if(login == null || string.IsNullOrWhiteSpace(login.Email) || string.IsNullOrWhiteSpace(login.Password))
            {
                return new ServiceResponse<Token> { Message = "Podane wartości nie mogą być puste", Success = false };
            }
            UserModel? user = await GetUser(login.Email);
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
            UserModel user = new()
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
        public async Task<SimpleServiceResponse> ChangePasswordAsync(ChangePassword changePassword)
        {
            string? userId = GetUserId();
            if(userId == null)
                return new ServiceResponse<Token> { Message = "Coś poszło nie tak - nie można zmienić hasła", Success = false };
            SimpleServiceResponse response = authentication.PasswordPolicyCheck(changePassword);
            if (!response.Success)
                return response;
            UserModel? user = await GetUserByIdAsync(userId);
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
            if(identity == null)
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