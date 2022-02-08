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
    }
}
