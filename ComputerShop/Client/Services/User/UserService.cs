using ComputerShop.Shared.Models;
using ComputerShop.Shared.Models.User;
using System.Net;
using System.Net.Http.Json;

namespace ComputerShop.Client.Services.User
{
    public class UserService : IUserService
    {
        private readonly HttpClient httpClient;
        public UserService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<ServiceResponse<string>?> Login(Login login)
        {
            using var response = await httpClient.PostAsJsonAsync("api/User/login", login);
            return await response.Content.ReadFromJsonAsync<ServiceResponse<string>>();
        }
        public async Task<SimpleServiceResponse?> Register(Register register)
        {
            using var response = await httpClient.PostAsJsonAsync("api/user/register", register);
            return await response.Content.ReadFromJsonAsync<SimpleServiceResponse>();
        }
        public async Task<ServiceResponse<HttpStatusCode>> ChangePassword(ChangePassword changePassword)
        {
            using var response = await httpClient.PostAsJsonAsync("api/user/changePassword", changePassword);
            var simple = await response.Content.ReadFromJsonAsync<SimpleServiceResponse>();
            return new ServiceResponse<HttpStatusCode> 
            { 
                Data = response.StatusCode, 
                Message = simple?.Message, 
                Success = simple?.Success ?? false 
            };
        }
        public async Task<bool> CheckAuthentication()
        {
            var respone = await httpClient.GetFromJsonAsync<SimpleServiceResponse>("api/user/checkAuthentication");
            return respone?.Success ?? false;
        }
        public async Task<bool> Logout()
        {
            var respone = await httpClient.GetAsync("api/user/logout");
            return respone?.IsSuccessStatusCode ?? false;
        }
        public async Task<List<UserModel>> GetAllUsersAsync()
        {
            var response = await httpClient.GetFromJsonAsync<ServiceResponse<List<UserModel>>>($"api/user/getAllUsers");
            if (response?.Success ?? false)
                return response?.Data ?? new List<UserModel>();
            return new List<UserModel>();
        }
    }
}