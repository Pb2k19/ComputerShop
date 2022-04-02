using ComputerShop.Shared.Models;
using ComputerShop.Shared.Models.User;

namespace ComputerShop.Client.Services
{
    public interface IUserService
    {
        Task<List<User>> GetAllUsers();
        Task<ServiceResponse<string>> Register(Register register);
        Task<bool> UserExists(string email);
    }
}
