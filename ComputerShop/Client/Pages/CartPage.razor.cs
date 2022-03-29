using ComputerShop.Shared.Models;
using ComputerShop.Client.Helpers;
using Microsoft.AspNetCore.Components;

namespace ComputerShop.Client.Pages
{
    public partial class CartPage
    {
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
            await CartService.RemoveItemFromCartAsync(productId);
        }

        public void Dispose()
        {
            CartService.OnUpdate -= CartServiceOnUpdateAsync;
        }
    }
}
