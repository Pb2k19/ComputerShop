using ComputerShop.Shared.Models;
using ComputerShop.Shared.Models.User;

namespace ComputerShop.Client.Services.Authentication
{
    public interface IAuthenticationService
    {
        Task<ServiceResponse<string>?> Register(Register register);
        Task<ServiceResponse<string>?> Login(Login login);
    }
}
