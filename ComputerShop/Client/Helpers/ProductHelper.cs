using ComputerShop.Shared.Models.Products;
using ComputerShop.Client.Services.Products;
using System.Collections.Immutable;

namespace ComputerShop.Client.Helpers
{
    public static class ProductHelper
    {
        private const string
            Pc = "pc",
            Psu = "psu",
            Gpu = "gpu",
            Laptop = "laptop",
            Cpu = "cpu",
            Motherboard = "motherboard",
            Ram = "ram",
            Hdd = "hdd",
            Ssd = "ssd",
            Case = "case",
            Keyboard = "keyboard",
            Headphones = "headphones",
            Monitor = "monitor",
            Cabel = "cabel",
            Cooler = "cooler",
            Mouse = "mouse";

        private static readonly ImmutableArray<string> categories = ImmutableArray.Create(Pc, Psu, Gpu, Laptop, Cpu, Motherboard, Ram, Hdd, Ssd, Case, Keyboard, Headphones, Monitor, Cabel, Cooler, Mouse);

        public static async Task<Product?> GetProductByCategoryAndIdAsync(IProductsService productsService, string categoryName, string id)
        {
            return categoryName.ToLower() switch
            {
                Pc => await productsService.GetProductByIdAsync<DesktopPcProduct>(id),
                Psu => await productsService.GetProductByIdAsync<DesktopPsuProduct>(id),
                Gpu => await productsService.GetProductByIdAsync<DesktopGpuProduct>(id),
                Laptop => await productsService.GetProductByIdAsync<LaptopProduct>(id),
                Cpu => await productsService.GetProductByIdAsync<CpuProduct>(id),
                Motherboard => await productsService.GetProductByIdAsync<MotherboardProduct>(id),
                Ram => await productsService.GetProductByIdAsync<RamProduct>(id),
                Hdd => await productsService.GetProductByIdAsync<HddProduct>(id),
                Ssd => await productsService.GetProductByIdAsync<SsdProduct>(id),
                Case => await productsService.GetProductByIdAsync<DesktopCaseProduct>(id),
                Keyboard => await productsService.GetProductByIdAsync<KeyboardProduct>(id),
                Headphones => await productsService.GetProductByIdAsync<HeadphonesProduct>(id),
                Monitor => await productsService.GetProductByIdAsync<MonitorProduct>(id),
                Cabel => await productsService.GetProductByIdAsync<CableProduct>(id),
                Cooler => await productsService.GetProductByIdAsync<DesktopCoolerProduct>(id),
                Mouse => await productsService.GetProductByIdAsync<ComputerMouseProduct>(id),
                _ => await productsService.GetProductByIdAsync(id),
            };
        }
        public static Product? GetNewProductByCategory(string categoryName)
        {
            return categoryName.ToLower() switch
            {
                Pc => new DesktopPcProduct(),
                Psu => new DesktopPsuProduct(),
                Gpu => new DesktopGpuProduct(),
                Laptop => new LaptopProduct(),
                Cpu => new CpuProduct(),
                Motherboard => new MotherboardProduct(),
                Ram => new RamProduct(),
                Hdd => new HddProduct(),
                Ssd => new SsdProduct(),
                Case => new DesktopCaseProduct(),
                Keyboard => new KeyboardProduct(),
                Headphones => new HeadphonesProduct(),
                Monitor => new MonitorProduct(),
                Cabel => new CableProduct(),
                Cooler => new DesktopCoolerProduct(),
                Mouse => new ComputerMouseProduct(),
                _ => new Product(),
            };
        }
        
        public static ImmutableArray<string> GetCategories()
        {
            return categories;
        }
    }
}