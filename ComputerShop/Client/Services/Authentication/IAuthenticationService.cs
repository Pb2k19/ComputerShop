using ComputerShop.Shared.Models;
using ComputerShop.Shared.Models.User;
using System.Net;

namespace ComputerShop.Client.Services.Authentication
{
    public interface IAuthenticationService
    {
        Task<ServiceResponse<string>?> Login(Login login);
        Task<SimpleServiceResponse?> Register(Register register);
        Task<ServiceResponse<HttpStatusCode>> ChangePassword(ChangePassword changePassword);
    }
}
