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
                        "PC" => await ProductsService.GetProductGeneric<DesktopPcProduct>(id),
                        "PSU" => await ProductsService.GetProductGeneric<DesktopPsuProduct>(id),
                        "GPU" => await ProductsService.GetProductGeneric<DesktopGpuProduct>(id),
                        "Laptop" => await ProductsService.GetProductGeneric<LaptopProduct>(id),
                        "CPU" => await ProductsService.GetProductGeneric<CpuProduct>(id),
                        "Motherboard" => await ProductsService.GetProductGeneric<MotherboardProduct>(id),
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
