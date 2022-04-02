using ComputerShop.Shared.Models;
using ComputerShop.Client.Helpers;
using Microsoft.AspNetCore.Components;
using Blazored.Toast.Services;

namespace ComputerShop.Client.Pages
{
    public partial class CartPage
    {
        protected delegate void Page_Load();

        [Inject] IToastService? ToastService { get; set; }
        [Inject] NavigationManager? NavigationManager { get; set; }
        private List<ProductCartItem> productCartItems = new();

        protected override async Task OnInitializedAsync()
        {
            productCartItems = await CartService.GetCartProductsAsync();
            CartService.OnUpdate += CartServiceOnUpdateAsync;
            base.OnInitialized();
        }

        private async void CartServiceOnUpdateAsync()
        {
            productCartItems = await CartService.GetCartProductsAsync();
            StateHasChanged();
        }

        protected void GoToProductPage(string productId)
        {
            Product? product = productCartItems.FirstOrDefault(x => productId.Equals(x.Product.Id))?.Product;
            NavigationManager?.GoToProductPage(productId, product?.Category?.Name);
        }

        protected async Task OnValueChangedAsync(int value, string productId)
        {
            await CartService.UpdateCartItemQuantityAsync(value, productId);
        }

        protected async Task OnItemRemoveAsync(string productId)
        {
            var response = await CartService.RemoveItemFromCartAsync(productId);
            if(response.Success)
                await InvokeAsync(() => ToastService?.ShowCartItemRemoved());
            else
                await InvokeAsync(() => ToastService?.ShowError(string.Empty, response.Message));
        }

        public void Dispose()
        {
            CartService.OnUpdate -= CartServiceOnUpdateAsync;
        }
    }
}
