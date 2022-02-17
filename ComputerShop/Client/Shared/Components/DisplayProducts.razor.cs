using ComputerShop.Shared.Models;
using Microsoft.AspNetCore.Components;

namespace ComputerShop.Client.Shared.Components
{
    public partial class DisplayProducts
    {
        [Inject] NavigationManager? NavigationManager { get; set; }
        [Parameter] public List<Product> Products { get; set;} = new();

        private void OnProductCardClicked(int productId)
        {
            Product? product = Products?.FirstOrDefault(x => x.Id == productId);
            if(product == null || product.Category == null)
                NavigationManager?.NavigateTo($"/product/{productId}", false);
            else
                NavigationManager?.NavigateTo($"/product/{product.Category.Name}/{productId}", false);
        }
        private void AddProductToCard(int productId)
        {
            Console.WriteLine($"New product in card of id:{productId}");
        }
    }
}