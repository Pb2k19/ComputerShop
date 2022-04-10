using Blazored.LocalStorage;
using System.Security.Claims;
using System.Net.Http.Headers;
using ComputerShop.Client.Helpers;
using Microsoft.AspNetCore.Components.Authorization;

namespace ComputerShop.Client
{
    public class ComputerShopAuthenticationStateProvider : AuthenticationStateProvider
    {
        readonly HttpClient httpClient;
        readonly ILocalStorageService localStorageService;
        public ComputerShopAuthenticationStateProvider(HttpClient httpClient, ILocalStorageService localStorageService)
        {
            this.httpClient = httpClient;   
            this.localStorageService = localStorageService;
        }
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            ClaimsIdentity claimsIdentity = new();
            string token = await localStorageService.GetItemAsStringAsync("jwt");            
            if(!string.IsNullOrWhiteSpace(token))
            {
                try
                {
                    token = token.Replace("\"", string.Empty);
                    claimsIdentity = new ClaimsIdentity(JwtHelper.GetClaimsFromJwt(token), "jwt");
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                }
                catch (Exception)
                {
                    claimsIdentity = new();
                    await localStorageService.RemoveItemAsync("jwt");
                }
            }
            ClaimsPrincipal? claimsPrincipal = new(claimsIdentity);
            AuthenticationState? authenticationState = new(claimsPrincipal);
            NotifyAuthenticationStateChanged(Task.FromResult(authenticationState));
            return authenticationState;
        }
    }
}
