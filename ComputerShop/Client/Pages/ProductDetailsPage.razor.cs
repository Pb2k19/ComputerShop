using ComputerShop.Client.Services;
using ComputerShop.Shared.Models;
using Microsoft.AspNetCore.Components;

namespace ComputerShop.Client.Pages
{
    public partial class ProductDetailsPage
    {
        [Parameter] public string? Id { get; set; }
        [Inject] IProductsService? ProductsService { get; set; }
        private Product? currentProdcut;
        protected override void OnInitialized()
        {
            base.OnInitialized();
            if(ProductsService?.Products is null || ProductsService?.Products.Count == 0)
            {
                ProductsService?.Load();
            }
            if (int.TryParse(Id, out int id))
            {
                currentProdcut = ProductsService?.Products?.FirstOrDefault(x => x.Id == id);
            }
        }
    }
}
