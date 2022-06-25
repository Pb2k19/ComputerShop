using ComputerShop.Shared.Models;
using ComputerShop.Shared.Models.Products;
using Microsoft.AspNetCore.Components;

namespace ComputerShop.Client.Pages
{
    public partial class Index
    {
        private List<Image> images = new() 
        { 
            new Image() { Location = "images/mainpage_img1.png" }, 
            new Image() { Location = "images/mainpage_img2.png" },
        };
        private List<Product> higlightedProducts = new();

        protected override async Task OnInitializedAsync()
        {
            higlightedProducts = await ProductsService.GetAllHiglightedProductsAsync();
        }
    }
}
