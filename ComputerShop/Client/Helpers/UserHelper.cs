using Blazored.LocalStorage;
using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace ComputerShop.Client.Helpers
{
    public static class UserHelper
    {
        static public async Task LogOutOnUnauthorized(ILocalStorageService? localStorageService, 
            AuthenticationStateProvider? stateProvider, IToastService? toastService, NavigationManager? navigationManager)
        {
            await LogOut(localStorageService, stateProvider);
                toastService?.ShowError("Nie możemy potwierdzić twojej tożsamości", "Zaloguj się ponownie");
            navigationManager?.GoToLoginPage();
        }
        static public async Task LogOut(ILocalStorageService? localStorageService, AuthenticationStateProvider? stateProvider, 
            IToastService? toastService, NavigationManager? navigationManager, string message = "", string title = "", string path = "", bool isError = false)
        {
            await LogOut(localStorageService, stateProvider);
            if(!isError)
                toastService?.ShowInfo(message, title);
            else
                toastService?.ShowError(message, title);
            navigationManager?.NavigateTo(path);
        }
        static public async Task LogOut(ILocalStorageService? localStorageService, AuthenticationStateProvider? stateProvider)
        {
            if (localStorageService == null)
                throw new ArgumentNullException(nameof(localStorageService));
            await localStorageService.RemoveItemAsync("jwt");
            if (stateProvider == null)
                throw new ArgumentNullException(nameof(stateProvider));
            await stateProvider.GetAuthenticationStateAsync();
        }
    }
}
