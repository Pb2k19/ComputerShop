using ComputerShop.Shared.Models.Products;
using ComputerShop.Client.Services.Products;
using ComputerShop.Shared.Models.Interfaces;


namespace ComputerShop.Client.Helpers
{
    public static class ProductHelper
    {
        public static async Task<IProduct?> GetProductByCategoryAndIdAsync(IProductsService productsService, string categoryName, string id)
        {
             return categoryName.ToLower() switch
            {
                "pc" => await productsService.GetProductByIdAsync<DesktopPcProduct>(id),
                "psu" => await productsService.GetProductByIdAsync<DesktopPsuProduct>(id),
                "gpu" => await productsService.GetProductByIdAsync<DesktopGpuProduct>(id),
                "laptop" => await productsService.GetProductByIdAsync<LaptopProduct>(id),
                "cpu" => await productsService.GetProductByIdAsync<CpuProduct>(id),
                "motherboard" => await productsService.GetProductByIdAsync<MotherboardProduct>(id),
                "ram" => await productsService.GetProductByIdAsync<RamProduct>(id),
                "hdd" => await productsService.GetProductByIdAsync<HddProduct>(id),
                "ssd" => await productsService.GetProductByIdAsync<SsdProduct>(id),
                "case" => await productsService.GetProductByIdAsync<DesktopCaseProduct>(id),
                "keyboard" => await productsService.GetProductByIdAsync<KeyboardProduct>(id),
                "headphones" => await productsService.GetProductByIdAsync<HeadphonesProduct>(id),
                "monitor" => await productsService.GetProductByIdAsync<MonitorProduct>(id),
                "printer" => await productsService.GetProductByIdAsync<PrinterProduct>(id),
                "cabel" => await productsService.GetProductByIdAsync<CableProduct>(id),
                "tool" => await productsService.GetProductByIdAsync<ToolProduct>(id),
                "cooler" => await productsService.GetProductByIdAsync<DesktopCoolerProduct>(id),
                "mouse" => await productsService.GetProductByIdAsync<ComputerMouseProduct>(id),
                _ => await productsService.GetProductByIdAsync(id),
            };
        }
    }
}
