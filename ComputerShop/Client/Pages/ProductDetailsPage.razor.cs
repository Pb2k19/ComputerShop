using ComputerShop.Client.Services;
using ComputerShop.Shared.Models;
using Microsoft.AspNetCore.Components;

namespace ComputerShop.Client.Pages
{
    public partial class ProductDetailsPage
    {
        [Parameter] public string? Id { get; set; }

        private Product? currentProdcut;
        protected override async Task OnInitializedAsync()
        {
            if (int.TryParse(Id, out int id))
            {
                currentProdcut = await ProductsService.GetProductById(id);
            }
            base.OnInitialized();
        }
    }
}
