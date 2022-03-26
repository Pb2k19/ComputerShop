using ComputerShop.Shared.Models;

namespace ComputerShop.Client.Pages
{
    public partial class CartPage
    {
        private List<Product> products = new();

        protected override async Task OnInitializedAsync()
        {
            products = await CartService.GetCartProductsAsync();
            base.OnInitialized();
        }
    }
}
