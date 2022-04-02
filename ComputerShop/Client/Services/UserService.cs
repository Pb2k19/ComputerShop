using ComputerShop.Shared.Models;
using ComputerShop.Shared.Models.User;
using ComputerShop.Client.Helpers;

namespace ComputerShop.Client.Services
{
    public class UserService : IUserService
    {
        List<User> users = new()
        {
            new User { Id = "1", Email = "Andrzej0@wp.com" }
        };

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

        public async Task<ServiceResponse<string>> Register(Register register)
        {
            if(await UserExists(register.Email))
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
            CryptoHelper cryptoHelper = new();
            user.Password = await cryptoHelper.CreateBcryptHashAsync(register.Password);
            users.Add(user);
            return new ServiceResponse<string> { Data = user.Id };
        }
    }
}