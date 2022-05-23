using ComputerShop.Shared.Models.User;

namespace ComputerShop.Server.DataAccess
{
    public interface IUserData
    {
        Task CreateUser(UserModel user);
        Task<List<UserModel>> GetAllUsersAsync();
        Task<UserModel> GetUserByEmailAsync(string email);
        Task<RegisteredUser?> GetRegisteredUserByEmailAsync(string email);
        Task<UserModel> GetUserByIdAsync(string id);
        Task<RegisteredUser?> GetRegisteredUserByIdAsync(string id);
        Task<OrderModel?> GetOrderAsync(string id);
        Task<OrderModel?> GetOrderAsync(string orderId, string userId);
        Task UpdateOrderAsync(OrderModel order);
        Task UpdateOrderAsync(OrderModel order, string userId);
        Task UpdateUserAsync(UserModel user);
    }
}