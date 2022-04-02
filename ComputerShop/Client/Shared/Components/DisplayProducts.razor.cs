using ComputerShop.Shared.Models;
using ComputerShop.Client.Helpers;
using Microsoft.AspNetCore.Components;
using Blazored.Toast.Services;

namespace ComputerShop.Client.Shared.Components
{
    public partial class DisplayProducts
    {
        [Inject] NavigationManager? NavigationManager { get; set; }
        [Inject] IToastService? toastService { get; set; }
        [Parameter] public List<Product> Products { get; set;} = new();

        private void OnProductCardClicked(string productId)
        {
            Product? product = Products?.FirstOrDefault(x => x.Id != null && x.Id.Equals(productId));
            NavigationManager?.GoToProductPage(productId, product?.Category?.Name);
        }
        private async void AddProductToCart(string productId, decimal price)
        {
            var response = await CartService.AddItemToCartAsync(new CartItem { ProductId = productId, Price = price, Quantity = 1 });
            if (response.Success)
                toastService?.ShowAddToCart(Products.FirstOrDefault(x => x.Id.Equals(productId))?.Name ?? "");
            else
                await InvokeAsync(() => toastService?.ShowError(string.Empty, response.Message));
        }
    }
}