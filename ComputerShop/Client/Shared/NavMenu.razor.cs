using Blazored.LocalStorage;
using Blazored.Toast.Services;
using ComputerShop.Shared.Models;
using Microsoft.AspNetCore.Components;
using ComputerShop.Client.Services.Cart;
using Microsoft.AspNetCore.Components.Authorization;


namespace ComputerShop.Client.Shared
{
    public partial class NavMenu : IDisposable
    {
        [Inject] ICartService? CartService { get; set; }
        [Inject] ILocalStorageService? LocalStorageService { get; set; }
        [Inject] AuthenticationStateProvider? StateProvider { get; set; }
        [Inject] NavigationManager? NavigationManager { get; set; }
        [Inject] IToastService? ToastService { get; set; }


        DropdownNavigationItems PeripheryList = new(new List<DropdownNavigationItem> 
        {
            new DropdownNavigationItem{Name = "Myszy", Path = "mouse" },
            new DropdownNavigationItem{Name = "Klawiatury", Path = "keyboard" },
            new DropdownNavigationItem{Name = "Słuchawki", Path = "headphones" },
            new DropdownNavigationItem{Name = "Monitory", Path = "montior" },
            new DropdownNavigationItem{Name = "Drukarki", Path = "printer" }, 
        });

        DropdownNavigationItems ComponentsList = new(new List<DropdownNavigationItem>
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

        DropdownNavigationItems AccountOptions = new(new List<DropdownNavigationItem>
        {
            new DropdownNavigationItem { Name = "Szczegóły", Path = "account/details" },
            new DropdownNavigationItem { Name = "Zamówienia", Path = "account/orders" },
            new DropdownNavigationItem { Name = "Lista życzeń", Path = "account/wishlist" },
#pragma warning disable CS8625
            null,
#pragma warning restore CS8625
            new DropdownNavigationItem { Name = "Wyloguj", Path = "logout" }
        });

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
            string? url = NavigationManager?.ToBaseRelativePath(NavigationManager.Uri);
            if(string.IsNullOrWhiteSpace(url))
                NavigationManager?.NavigateTo($"login");
            else
                NavigationManager?.NavigateTo($"login?return-page={url}");
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
            string path = AccountOptions.GetPathByIndex(id);
            if(!string.IsNullOrWhiteSpace(path))
            {
                if(path.Equals("logout"))
                {
                    if (LocalStorageService == null || StateProvider == null)
                        throw new Exception();
                    await LocalStorageService.RemoveItemAsync("jwt");
                    await StateProvider.GetAuthenticationStateAsync();
                    ToastService?.ShowInfo(string.Empty, "Wylogowano");
                    NavigationManager?.NavigateTo("");
                }
                else
                {
                    NavigationManager?.NavigateTo(path);
                }
            }
        }
    }
}