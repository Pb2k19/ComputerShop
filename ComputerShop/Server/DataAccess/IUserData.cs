using ComputerShop.Shared.Models.User;

namespace ComputerShop.Server.DataAccess
{
    public interface IUserData
    {
        Task CreateUser(UserModel user);
        Task<List<UserModel>> GetAllUsersAsync();
        Task<RegisteredUser?> GetRegisteredUserByEmailAsync(string email);
        Task<UserModel> GetUserByIdAsync(string id);
        Task<RegisteredUser?> GetRegisteredUserByIdAsync(string id);
        Task<OrderModel?> GetOrderAsync(string id);
        Task<OrderModel?> GetOrderAsync(string orderId, string userId);
        Task UpdateOrderAsync(OrderModel order);
        Task UpdateOrderAsync(OrderModel order, string userId);
        Task UpdateUserAsync(UserModel user);
        Task<OrderModel?> GetFirstUnpaidOrderAsync(string userId);
        Task<RegisteredUser?> GetAnyUserByEmailAsync(string email);
    }
}