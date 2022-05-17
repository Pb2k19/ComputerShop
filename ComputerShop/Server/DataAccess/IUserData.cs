using ComputerShop.Shared.Models.User;

namespace ComputerShop.Server.DataAccess
{
    public interface IUserData
    {
        Task CreateUser(UserModel user);
        Task<List<UserModel>> GetAllUsersAsync();
        Task<UserModel> GetUserByEmailAsync(string email);
        Task<UserModel> GetUserByIdAsync(string id);
        Task UpdateUser(UserModel user);
    }
}