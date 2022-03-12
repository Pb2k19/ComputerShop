using ComputerShop.Client.Services;
using ComputerShop.Shared.Models.Interfaces;
using ComputerShop.Shared.Models.Products;

namespace ComputerShop.Client.Helpers
{
    public static class ProductHelper
    {
        public static async Task<IProduct?> GetProductByCategoryAndIdAsync(IProductsService productsService, string categoryName, int id)
        {
             return categoryName.ToLower() switch
            {
                "pc" => await productsService.GetProductGeneric<DesktopPcProduct>(id),
                "psu" => await productsService.GetProductGeneric<DesktopPsuProduct>(id),
                "gpu" => await productsService.GetProductGeneric<DesktopGpuProduct>(id),
                "laptop" => await productsService.GetProductGeneric<LaptopProduct>(id),
                "cpu" => await productsService.GetProductGeneric<CpuProduct>(id),
                "motherboard" => await productsService.GetProductGeneric<MotherboardProduct>(id),
                "ram" => await productsService.GetProductGeneric<RamProduct>(id),
                "hdd" => await productsService.GetProductGeneric<HddProduct>(id),
                "ssd" => await productsService.GetProductGeneric<SsdProduct>(id),
                "case" => await productsService.GetProductGeneric<DesktopCaseProduct>(id),
                "keyboard" => await productsService.GetProductGeneric<KeyboardProduct>(id),
                "headphones" => await productsService.GetProductGeneric<HeadphonesProduct>(id),
                "monitor" => await productsService.GetProductGeneric<MonitorProduct>(id),
                "printer" => await productsService.GetProductGeneric<PrinterProduct>(id),
                "cabel" => await productsService.GetProductGeneric<CableProduct>(id),
                "tool" => await productsService.GetProductGeneric<ToolProduct>(id),
                "cooler" => await productsService.GetProductGeneric<DesktopCoolerProduct>(id),
                "mouse" => await productsService.GetProductGeneric<ComputerMouseProduct>(id),
                _ => await productsService.GetProductById(id),
            };
        }
    }
}
