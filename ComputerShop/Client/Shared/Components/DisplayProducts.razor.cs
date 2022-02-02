using ComputerShop.Client.Services;
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
            NavigationManager?.NavigateTo($"/product/{productId}", false);
        }
        private void AddProductToCard(int productId)
        {
            Console.WriteLine($"New product in card of id:{productId}");
        }
    }
}