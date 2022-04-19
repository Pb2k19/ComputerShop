using ComputerShop.Shared.Models;
using ComputerShop.Client.Helpers;
using Microsoft.AspNetCore.Components;
using Blazored.Toast.Services;

namespace ComputerShop.Client.Pages
{
    public partial class SummaryPage
    {
        [Inject] IToastService? ToastService { get; set; }
        [Inject] NavigationManager? NavigationManager { get; set; }
        private List<ProductCartItem> productCartItems = new();
        private decimal total = 0;

        protected override async Task OnInitializedAsync()
        {
            productCartItems = await CartService.GetCartProductsAsync();
            UpdatePrice();
            CartService.OnUpdate += CartServiceOnUpdateAsync;
            base.OnInitialized();
        }

        private async void CartServiceOnUpdateAsync()
        {
            productCartItems = await CartService.GetCartProductsAsync();
            UpdatePrice();
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
            if (response.Success)
                await InvokeAsync(() => ToastService?.ShowCartItemRemoved());
            else
                await InvokeAsync(() => ToastService?.ShowError(string.Empty, response.Message));
        }

        private void UpdatePrice()
        {
            total = 0;
            productCartItems.ForEach(item => total += item.Product.Price * item.Quantity);
        }

        public void Dispose()
        {
            CartService.OnUpdate -= CartServiceOnUpdateAsync;
        }
    }
}
