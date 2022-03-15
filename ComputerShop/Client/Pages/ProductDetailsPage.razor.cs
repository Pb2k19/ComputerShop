using ComputerShop.Client.Helpers;
using ComputerShop.Shared.Models.Interfaces;
using Microsoft.AspNetCore.Components;

namespace ComputerShop.Client.Pages
{
    public partial class ProductDetailsPage
    {
        [Parameter] public string? Categoryname { get; set; }
        [Parameter] public string? Id { get; set; }        

        private IProduct? currentProdcut;
        protected override async Task OnInitializedAsync()
        {
            if(int.TryParse(Id, out int id))
            {                
                if (!string.IsNullOrWhiteSpace(Categoryname))
                {
                    currentProdcut = await ProductHelper.GetProductByCategoryAndIdAsync(ProductsService, Categoryname, id);
                }
                else
                {
                    currentProdcut = await ProductsService.GetProductByIdAsync(id);
                }
            }
            base.OnInitialized();
        }

        protected async Task OnListItemClick(int num)
        {
            switch (num)
            {
                case 0:
                    await JS.ScrollToElement("product-name");
                    break;
                case 1:
                    await JS.ScrollToElement("product-description");
                    break;
                case 2:
                    await JS.ScrollToElement("product-details");
                    break;
                case 3:
                    await JS.ScrollToElement("product-opinions");
                    break;
                default:
                    break;
            }
        }
    }
}
