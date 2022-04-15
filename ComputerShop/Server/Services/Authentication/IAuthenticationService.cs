using ComputerShop.Server.Models;
using ComputerShop.Shared.Models;
using ComputerShop.Shared.Models.User;
using System.Security.Principal;

namespace ComputerShop.Server.Services.Authentication
{
    public interface IAuthenticationService
    {
        bool ValidateJWT(HttpRequest request, IIdentity? identity);
        Task<ServiceResponse<Token>> Login (Login login);
        Task<SimpleServiceResponse> Register(Register register);
        Task<SimpleServiceResponse> ChangePassword(string userId, ChangePassword changePassword);
    }
}