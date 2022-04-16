using ComputerShop.Shared.Models;
using ComputerShop.Shared.Models.User;
using System.Net;

namespace ComputerShop.Client.Services.User
{
    public interface IUserService
    {
        Task<ServiceResponse<string>?> Login(Login login);
        Task<SimpleServiceResponse?> Register(Register register);
        Task<ServiceResponse<HttpStatusCode>> ChangePassword(ChangePassword changePassword);
    }
}
