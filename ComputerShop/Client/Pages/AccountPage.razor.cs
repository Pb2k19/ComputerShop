using ComputerShop.Client.Helpers;
using ComputerShop.Shared.Models;
using ComputerShop.Shared.Models.Products;
using ComputerShop.Shared.Models.User;
using Microsoft.AspNetCore.Components;

namespace ComputerShop.Client.Pages
{
    public partial class AccountPage
    {
        [Parameter] public string? Page { get; set; }
        [Parameter] public string? OrderId { get; set; }
        readonly NavigationItems navigationItems = new(new List<NavigationItem>
        {
            new NavigationItem { Name = "Zamówienia", Path="orders"},
            new NavigationItem { Name = "Lista życzeń", Path="wish-list"},
            new NavigationItem { Name = "Dane do dostawy", Path="delivery-details"},
            new NavigationItem { Name = "Dane do faktury", Path="invoice-details"},
            new NavigationItem { Name = "Bezpieczeństwo", Path="security"},
        });
        ChangePassword changePassword = new();
        IUserHelper? userHelper = null;
        DeliveryDetails delivery = new();
        InvoiceDetails invoiceDetails = new();
        List<OrderModel> orderList = new();
        OrderModel? currentOrder;
        List<ProductCartItem> currentProductCartItems = new();
        List<Product> wishListProducts = new();
        bool firstLoad = true;

        protected override void OnInitialized()
        {
            if (userHelper == null)
                userHelper = new UserHelper(StateProvider, UserService, LocalStorageService, NavigationManager, ToastService);            
        }
        protected override async Task OnParametersSetAsync()
        {
            await ChangeViewAsync(Page);
        }

        protected async Task OnPasswordChangeAsync()
        {
            if (userHelper == null)
                userHelper = new UserHelper(StateProvider, UserService, LocalStorageService, NavigationManager, ToastService);
            var response = await UserService.ChangePassword(changePassword);
            if (response.Success)
                ToastService.ShowSuccess("Hasło zostało zmienione", "Udało się!");
            else
            {
                if (response.Data == System.Net.HttpStatusCode.Unauthorized)
                    await userHelper.LogoutOnUnauthorizedAsync();
                else
                    ToastService.ShowInfo(response?.Message, "Nie udało się");
            }
        }
        public async Task ChangeViewAsync(string? name)
        {
            if(name != null && name.Equals(Page) && !firstLoad)
                return;
            firstLoad = false;
            Page = name;
            switch(name)
            {
                case "wish-list":
                    NavigationManager.NavigateTo("/account/wish-list");
                    await LoadWishListAsync();
                    break;
                case "delivery-details":
                    NavigationManager.NavigateTo("/account/delivery-details");
                    await LoadDeliveryDetailsAsync();
                    break;
                case "invoice-details":
                    NavigationManager.NavigateTo("/account/invoice-details");
                    await LoadInvoiceDetailsAsync();
                    break;
                case "order-details":
                    NavigationManager.NavigateTo($"/account/order-details/{OrderId}");
                    await LoadCurrentOderAsync();                                      
                    break;
                case "security":
                    NavigationManager.NavigateTo($"/account/security");
                    break;
                default:
                    NavigationManager.NavigateTo("/account");
                    await LoadOrdersAsync();
                    break;
            }
            if(!name?.Equals("security") ?? true)
            {
                changePassword.Clear();
            }
        }
        protected async void OnValidSubmitInvoiceAsync()
        {
            var re = await UserDetails.UpdateInvoiceDetailsAsync(invoiceDetails);
            if (re?.Success ?? false)
                ToastService.ShowSuccess("Zaktualizowano dane do faktury", "Sukces");
            else
                ToastService.ShowError(re?.Message ?? "Coś poszło nie tak", "Nie udało się");
        }
        protected async void OnValidSubmitDeliveryAsync()
        {
            var re = await UserDetails.UpdateDeliveryDetailsAsync(delivery);
            if (re?.Success ?? false)
                ToastService.ShowSuccess("Zaktualizowano dane do dostawy", "Sukces");
            else
                ToastService.ShowError(re?.Message ?? "Coś poszło nie tak", "Nie udało się");
        }
        protected async Task OnOrderClickAsync(string orderId)
        {
            OrderId = orderId;
            await ChangeViewAsync("order-details");
        }
        protected async Task OnWishListProductRemoveAsync(string productId)
        {            
            var re = await WishListService.RemoveFromWishListAsync(productId);
            if(re.Success)
            {
                wishListProducts.Remove(wishListProducts.First(w => w.Id.Equals(productId)));
            }
        }
        protected void GoToProductPage(string productId)
        {
            Product? product = currentProductCartItems.FirstOrDefault(x => productId.Equals(x.Product.Id))?.Product;
            NavigationManager?.GoToProductPage(productId, product?.Category);
        }
        private async Task LoadOrdersAsync()
        {
            orderList = new();
            var reO = await OrderService.GetAllOrdersForUserAsync();
            if (reO?.Success ?? false)
            {
                orderList = reO.Data?.OrderBy(p => p.OrderDate).ToList() ?? new();
            }
        }
        private async Task LoadCurrentOderAsync()
        {
            if (!string.IsNullOrEmpty(OrderId))
            {
                if (orderList == null || orderList.Count == 0)
                    await LoadOrdersAsync();
                currentOrder = orderList?.FirstOrDefault(o => o.Id.Equals(OrderId));
                if (currentOrder != null)
                    currentProductCartItems = await cartService.GetCartProductsAsync(currentOrder.CartItems, false, true);
            }
        }
        private async Task LoadInvoiceDetailsAsync()
        {
            invoiceDetails = new();
            var reI = await UserDetails.GetInvoiceDetailsAsync();
            if (reI?.Success ?? false)
            {
                invoiceDetails = reI.Data ?? new();
            }
        }
        private async Task LoadDeliveryDetailsAsync()
        {
            delivery = new();
            var reD = await UserDetails.GetDeliveryDetailsAsync();
            if (reD?.Success ?? false)
            {
                delivery = reD.Data ?? new();
            }
        }
        private async Task LoadWishListAsync()
        {
            WishListModel wishList = new();
            var reW = await WishListService.GetWishListAsync();
            if (reW.Success)
            {
                wishList = reW.Data ?? new();
                wishListProducts = (await productsService.GetProductsByIdListAsync(wishList.GetAllProductIds()))
                    .OrderBy(x => $"{x.Manufacturer} {x.Name}").ToList();
            }
        }
        private async Task AddToCartAsync(string productId, decimal price)
        {
            var response = await cartService.AddItemToCartAsync(new CartItem { ProductId = productId, Price = price, Quantity = 1 });
            if (response.Success)
                ToastService?.ShowAddToCart(wishListProducts.FirstOrDefault(x => x.Id.Equals(productId))?.Name ?? "");
            else
                await InvokeAsync(() => ToastService?.ShowError(string.Empty, response.Message));
        }
    }
}
