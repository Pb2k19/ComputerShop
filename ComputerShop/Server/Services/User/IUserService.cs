using ComputerShop.Server.Models;
using ComputerShop.Shared.Models;
using ComputerShop.Shared.Models.User;

namespace ComputerShop.Server.Services.User
{
    public interface IUserService
    {
        bool ValidateJWT(HttpRequest request);
        Task<ServiceResponse<Token>> Login (Login login);
        Task<SimpleServiceResponse> Register(Register register);
        Task<SimpleServiceResponse> ChangePassword(ChangePassword changePassword);
    }
}