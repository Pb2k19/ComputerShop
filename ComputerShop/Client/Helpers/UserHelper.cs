using Blazored.LocalStorage;
using Blazored.Toast.Services;
using ComputerShop.Client.Services.User;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace ComputerShop.Client.Helpers
{
    public interface IUserHelper
    {
        Task LogoutAsync();
        Task LogoutOnUnauthorizedAsync();
        Task LogoutAsync(string message = "", string title = "", string path = "", bool isError = false);
    }
    public class UserHelper : IUserHelper
    {
        private readonly AuthenticationStateProvider? stateProvider;
        private readonly NavigationManager? navigationManager;
        private readonly ILocalStorageService? localStorageService;        
        private readonly IToastService? toastService;        
        private readonly IUserService? userService;

        public UserHelper(AuthenticationStateProvider? stateProvider, IUserService? userService,
            ILocalStorageService? localStorageService, NavigationManager? navigationManager, IToastService? toastService )
        {
            this.stateProvider = stateProvider;
            this.navigationManager = navigationManager;
            this.localStorageService = localStorageService;
            this.toastService = toastService;
            this.userService = userService;
        }

        public async Task LogoutOnUnauthorizedAsync()
        {
            await LogoutAsync();
            toastService?.ShowError("Nie możemy potwierdzić twojej tożsamości", "Zaloguj się ponownie");
            navigationManager?.GoToLoginPage();
        }
        public async Task LogoutAsync(string message = "", string title = "", string path = "", bool isError = false)
        {
            await LogoutAsync();
            if(!isError)
                toastService?.ShowInfo(message, title);
            else
                toastService?.ShowError(message, title);
            navigationManager?.NavigateTo(path, forceLoad: true);
        }
        public async Task LogoutAsync()
        {
            if(userService == null)
                throw new ArgumentNullException(nameof(userService));
            await userService.Logout();
            if (localStorageService == null)
                throw new ArgumentNullException(nameof(localStorageService));
            await localStorageService.RemoveItemAsync("jwt");
            if (stateProvider == null)
                throw new ArgumentNullException(nameof(stateProvider));
            await stateProvider.GetAuthenticationStateAsync();
        }
    }
}
