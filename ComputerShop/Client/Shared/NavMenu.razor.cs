using Blazored.LocalStorage;
using Blazored.Toast.Services;
using ComputerShop.Shared.Models;
using ComputerShop.Client.Helpers;
using Microsoft.AspNetCore.Components;
using ComputerShop.Client.Services.Cart;
using Microsoft.AspNetCore.Components.Authorization;
using ComputerShop.Client.Services.User;

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

        readonly DropdownNavigationItems PeripheryList = new(new List<DropdownNavigationItem> 
        {
            new DropdownNavigationItem{Name = "Myszy", Path = "mouse" },
            new DropdownNavigationItem{Name = "Klawiatury", Path = "keyboard" },
            new DropdownNavigationItem{Name = "Słuchawki", Path = "headphones" },
            new DropdownNavigationItem{Name = "Monitory", Path = "montior" },
            new DropdownNavigationItem{Name = "Drukarki", Path = "printer" }, 
        });
        readonly DropdownNavigationItems ComponentsList = new(new List<DropdownNavigationItem>
        {
            new DropdownNavigationItem { Name = "Procesory", Path = "cpu" },
            new DropdownNavigationItem { Name = "Karty graficzne", Path = "gpu" },
            new DropdownNavigationItem { Name = "Płyty główne", Path = "motherboard" },
            new DropdownNavigationItem { Name = "Ram", Path = "ram" },
            new DropdownNavigationItem { Name = "Zasilacze", Path = "psu" },
            new DropdownNavigationItem { Name = "Dyski HDD", Path = "hdd" },
            new DropdownNavigationItem { Name = "Dyski SSD", Path = "ssd" },
            new DropdownNavigationItem { Name = "Obudowy", Path = "case" },
            new DropdownNavigationItem { Name = "Chłodzenie", Path = "cooler" },
            new DropdownNavigationItem { Name = "Przewody", Path = "cabel" },
        });
        readonly DropdownNavigationItems AccountOptions = new(new List<DropdownNavigationItem>
        {
            new DropdownNavigationItem { Name = "Szczegóły", Path = "account" },
            new DropdownNavigationItem { Name = "Zamówienia", Path = "account/orders" },
            new DropdownNavigationItem { Name = "Lista życzeń", Path = "account/wishlist" },
#pragma warning disable CS8625
            null,
#pragma warning restore CS8625
            new DropdownNavigationItem { Name = "Wyloguj", Path = "logout" }
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
        protected override void OnAfterRender(bool firstRender)
        {
            base.OnAfterRender(firstRender);
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