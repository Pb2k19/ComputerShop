using ComputerShop.Shared.Models;
using ComputerShop.Shared.Models.User;
using System.Net.Http.Json;

namespace ComputerShop.Client.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly HttpClient httpClient;
        public AuthenticationService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public async Task<ServiceResponse<string>?> Register(Register register)
        {
            using var response = await httpClient.PostAsJsonAsync("register", register);
            var result = await response.Content.ReadFromJsonAsync<ServiceResponse<string>>();
            return result;
        }
    }
}