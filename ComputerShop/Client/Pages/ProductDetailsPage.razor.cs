using ComputerShop.Client.Helpers;
using ComputerShop.Shared.Models;
using ComputerShop.Shared.Models.Products;
using Microsoft.AspNetCore.Components;

namespace ComputerShop.Client.Pages
{
    public partial class ProductDetailsPage
    {
        [Parameter] public string? Categoryname { get; set; }
        [Parameter] public string? Id { get; set; }        

        private Product? currentProdcut;
        protected override async Task OnInitializedAsync()
        {
            if(int.TryParse(Id, out int id))
            {                
                if (!string.IsNullOrWhiteSpace(Categoryname))
                {
                    currentProdcut = Categoryname switch
                    {
                        "Computers" => await ProductsService.GetProductGeneric<DesktopPcProduct>(id),
                        "PSU" => await ProductsService.GetProductGeneric<DesktopPsuProduct>(id),
                        _ => await ProductsService.GetProductById(id),
                    };
                }
                else
                {
                    currentProdcut = await ProductsService.GetProductById(id);
                }
            }
            base.OnInitialized();
        }

        protected async Task OnListItemClick(int num)
        {
            switch (num)
            {
                case 0:
                    await JS.ScrollToElement("product-description");
                    break;
                case 1:
                    await JS.ScrollToElement("product-details");
                    break;
                case 2:
                    await JS.ScrollToElement("product-opinions");
                    break;
                default:
                    break;
            }
        }
    }
}
