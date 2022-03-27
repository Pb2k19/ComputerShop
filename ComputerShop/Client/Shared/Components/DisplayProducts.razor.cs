using ComputerShop.Shared.Models;
using ComputerShop.Client.Helpers;
using Microsoft.AspNetCore.Components;

namespace ComputerShop.Client.Shared.Components
{
    public partial class DisplayProducts
    {
        [Inject] NavigationManager? NavigationManager { get; set; }
        [Parameter] public List<Product> Products { get; set;} = new();

        private void OnProductCardClicked(string productId)
        {
            Product? product = Products?.FirstOrDefault(x => x.Id != null && x.Id.Equals(productId));
            NavigationManager?.GoToProductPage(productId, product?.Category?.Name);
        }
        private void AddProductToCart(string productId, decimal price)
        {
            CartService.AddItemToCartAsync(new CartItem { ProductId = productId, Price = price, Quantity=1 });
        }
    }
}