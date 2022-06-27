using ComputerShop.Shared.Models.Products;
using ComputerShop.Client.Services.Products;
using ComputerShop.Shared.Models.Interfaces;
using ComputerShop.Shared.Models.Products.Interfaces;

namespace ComputerShop.Client.Helpers
{
    public static class ProductHelper
    {
        public static async Task<Product?> GetProductByCategoryAndIdAsync(IProductsService productsService, string categoryName, string id)
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
                "cabel" => await productsService.GetProductByIdAsync<CableProduct>(id),
                "cooler" => await productsService.GetProductByIdAsync<DesktopCoolerProduct>(id),
                "mouse" => await productsService.GetProductByIdAsync<ComputerMouseProduct>(id),
                _ => await productsService.GetProductByIdAsync(id),
            };
        }
        public static Product? GetNewProductByCategory(string categoryName)
        {
            return categoryName.ToLower() switch
            {
                "pc" => new DesktopPcProduct(),
                "psu" => new DesktopPsuProduct(),
                "gpu" => new DesktopGpuProduct(),
                "laptop" => new LaptopProduct(),
                "cpu" => new CpuProduct(),
                "motherboard" => new MotherboardProduct(),
                "ram" => new RamProduct(),
                "hdd" => new HddProduct(),
                "ssd" => new SsdProduct(),
                "case" => new DesktopCaseProduct(),
                "keyboard" => new KeyboardProduct(),
                "headphones" => new HeadphonesProduct(),
                "monitor" => new MonitorProduct(),
                "cabel" => new CableProduct(),
                "cooler" => new DesktopCoolerProduct(),
                "mouse" => new DesktopCoolerProduct(),
                _ => new Product(),
            };
        }
        
        public static List<string> GetCategories()
        {
            return new() { "pc", "psu", "gpu", "laptop", "cpu", "motherboard", "ram", "hdd", "ssd", "case", "keyboard", "headphones", "monitor", "cabel", "cooler", "mouse" };
        }
    }
}