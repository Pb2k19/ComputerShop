using Blazored.LocalStorage;
using Blazored.Toast.Services;
using ComputerShop.Shared.Models;
using ComputerShop.Client.Helpers;
using Microsoft.AspNetCore.Components;
using ComputerShop.Client.Services.Cart;
using Microsoft.AspNetCore.Components.Authorization;
using ComputerShop.Client.Services.User;
using System.Security.Claims;

namespace ComputerShop.Client.Shared
{
    public partial class NavMenu : IDisposable
    {
        [Inject] ICartService? CartService { get; set; }
        [Inject] ILocalStorageService? LocalStorageService { get; set; }
        [Inject] AuthenticationStateProvider? StateProvider { get; set; }
        [Inject] NavigationManager? NavigationManager { get; set; }
        [Inject] IToastService? ToastService { get; set; }
        [Inject] IUserService? UserService { get; set; }

        readonly NavigationItems PeripheryList = new(new List<NavigationItem> 
        {
            new NavigationItem{Name = "Myszy", Path = "categories/mouse" },
            new NavigationItem{Name = "Klawiatury", Path = "categories/keyboard" },
            new NavigationItem{Name = "Słuchawki", Path = "categories/headphones" },
            new NavigationItem{Name = "Monitory", Path = "categories/montior" },
        });
        readonly NavigationItems ComponentsList = new(new List<NavigationItem>
        {
            new NavigationItem { Name = "Procesory", Path = "categories/cpu" },
            new NavigationItem { Name = "Karty graficzne", Path = "categories/gpu" },
            new NavigationItem { Name = "Płyty główne", Path = "categories/motherboard" },
            new NavigationItem { Name = "Ram", Path = "categories/ram" },
            new NavigationItem { Name = "Zasilacze", Path = "categories/psu" },
            new NavigationItem { Name = "Dyski HDD", Path = "categories/hdd" },
            new NavigationItem { Name = "Dyski SSD", Path = "categories/ssd" },
            new NavigationItem { Name = "Obudowy", Path = "categories/case" },
            new NavigationItem { Name = "Chłodzenie", Path = "categories/cooler" },
            new NavigationItem { Name = "Przewody", Path = "categories/cabel" },
        });
        readonly NavigationItems AccountOptions = new(new List<NavigationItem>
        {
            new NavigationItem { Name = "Zamówienia", Path = "account/orders" },
            new NavigationItem { Name = "Szczegóły", Path = "account/delivery-details" },            
            new NavigationItem { Name = "Lista życzeń", Path = "account/wish-list" },
#pragma warning disable CS8625
            null,
#pragma warning restore CS8625
            new NavigationItem { Name = "Wyloguj", Path = "logout" }
        });
        IUserHelper? userHelper;
        bool collapseNavMenu = true;
        decimal cartValue = 0m;
        int cartCount = 0;
        string NavMenuCssClass => collapseNavMenu ? " collapse" : "";

        public void Dispose()
        {
            if(CartService != null)
                CartService.OnUpdate -= OnCartUpdate;
        }
        protected void ToggleNavMenu()
        {
            collapseNavMenu = !collapseNavMenu;
        }
        protected void CollapseNavMenu()
        {
            collapseNavMenu = true;
        }        
        protected override async Task OnInitializedAsync()
        {
            if (CartService != null)
            {
                CartService.OnUpdate += OnCartUpdate;
                await UpdateCartIconAsync();
            }
            base.OnInitialized();
        }        
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            base.OnAfterRender(firstRender);
            if (StateProvider is null)
                return;
            AuthenticationState? state = await StateProvider.GetAuthenticationStateAsync();
            if (state?.User.Claims?.FirstOrDefault(claim => claim.Type == ClaimTypes.Role)?.Value.Contains("Admin") ?? false)
            {
                if (AccountOptions.Items.Any(i => i?.Path?.Equals("admin") ?? false))
                    return;
                int index = AccountOptions.Items.IndexOf(null);
                if (index != -1)
                    AccountOptions.Items.Insert(index, new NavigationItem { Name = "Administracja", Path = "admin" });
            }
            else
            {
                var item = AccountOptions.Items.FirstOrDefault(i => i?.Path?.Equals("admin") ?? false);
                if (item is null)
                    return;
                AccountOptions.Items.Remove(item);
            }
        }
        protected async void OnCartUpdate()
        {
            await UpdateCartIconAsync();
        }
        private async Task UpdateCartIconAsync()
        {
            if (CartService is null)
                return;
            var cart = await CartService.GetCartInfoAsync();
            cartValue = cart.Item1;
            cartCount = cart.Item2;
            StateHasChanged();
        }
        protected void GoToLogin()
        {
            NavigationManager?.GoToLoginPage();
        }
        protected void OnPartsClicked(int id)
        {
            NavigationManager?.NavigateTo(ComponentsList.GetPathByIndex(id) ?? string.Empty);
        }
        protected void OnPeripheriesClicked(int id)
        {           
            NavigationManager?.NavigateTo(PeripheryList.GetPathByIndex(id) ?? string.Empty);
        }
        protected void OnAccountClicked()
        {
            NavigationManager?.NavigateTo("account");
        }
        protected async Task OnAccountOptionsClickedAsync(int id)
        {
            if (userHelper == null)
                userHelper = new UserHelper(StateProvider, UserService, LocalStorageService, NavigationManager, ToastService);
            string path = AccountOptions.GetPathByIndex(id);
            if(!string.IsNullOrWhiteSpace(path))
            {
                if(path.Equals("logout"))
                {
                    await userHelper.LogoutAsync(title: "Wylogowano");
                }
                else
                {
                    NavigationManager?.NavigateTo(path);
                }
            }
        }
    }
}