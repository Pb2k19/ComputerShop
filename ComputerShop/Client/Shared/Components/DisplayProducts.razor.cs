using ComputerShop.Client.Services;
using ComputerShop.Shared.Models;
using Microsoft.AspNetCore.Components;

namespace ComputerShop.Client.Shared.Components
{
    public partial class DisplayProducts
    {
        [Parameter] public int CategoryId { get; set; } = -1;
        [Inject] NavigationManager? NavigationManager { get; set; }
        [Inject] IProductsService? ProductsService { get; set; }

        private void OnProductCardClicked(int productId)
        {
            NavigationManager?.NavigateTo($"/product/{productId}", false);
        }
        protected override void OnInitialized()
        {
            base.OnInitialized();
            ProductsService?.Load();
        }
    }
}
