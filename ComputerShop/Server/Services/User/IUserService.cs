using ComputerShop.Server.Models;
using ComputerShop.Shared.Models;
using ComputerShop.Shared.Models.User;

namespace ComputerShop.Server.Services.User
{
    public interface IUserService
    {
        string? GetUserId();
        Task<UserModel?> GetUserByIdAsync(string id);
        Task<RegisteredUser?> GetRegisteredUserByIdAsync(string id);
        SimpleServiceResponse ValidateJWT(HttpRequest request);
        Task<ServiceResponse<Token>> LoginAsync (Login login);
        Task<SimpleServiceResponse> RegisterAsync(Register register);
        Task<SimpleServiceResponse> ChangePasswordAsync(ChangePassword changePassword);
        Task<SimpleServiceResponse> UpdateUserAsync(UserModel user);
        Task<SimpleServiceResponse> AddUserAsync(UserModel user);
    }
}