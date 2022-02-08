using ComputerShop.Shared.Models;
using ComputerShop.Shared.Models.Components;
using ComputerShop.Shared.Models.Products;

namespace ComputerShop.Server.Services
{
    public class ProductsServicecs : IProductsService
    {
        public List<Product> Products { get; set; } = new List<Product>
        {
            new DesktopPcProduct
            { 
                Id = 1,
                WarantyMonths = 24,
                Cpu = new Cpu
                {
                    CoresCount=8,
                    ThreadsCount = 16,
                    FrequencyMHz = 2500,
                    Manufacturer = "AMD",
                    L3CacheMB = 25,
                    Name = "Ryzen 7 2700",
                    Tdp = 150,
                },
                Description = "Amazing gaming desktop pc",
                Drives = new List<Drive>
                { 
                    new Ssd 
                    {
                        WriteSpeedMBs=500, 
                        Interface = "sata", 
                        ReadSpeedMBs = 500, 
                        Manufacturer = "WD",
                        Tbw = 500
                    },
                    new Ssd
                    {
                        WriteSpeedMBs=4000,
                        Interface = "M.2 NVME 3.0",
                        ReadSpeedMBs = 8000,
                        Manufacturer = "WD",
                        Tbw = 500
                    }
                },
                Gpu = new Gpu
                {
                    ChipManufacturer = GpuManufacturers.Nvidia.ToString(),
                    FrequencyMHz = 1500,
                    Manufacturer = "Asus",
                    MemoryFrequencyMHz = 4000,
                    Name = "GTX 1070",
                    Tdp = 150,
                    VramSizeGB = 8,
                    VramType = GpuVramTypes.GDDR5.ToString(),
                },
                Images = new List<Image>
                {
                    new Image { Location = "https://dlcdnwebimgs.asus.com/gain/75AF54F5-66D8-4A48-B785-65E7F8011C1E/w1000/h732"},
                    new Image { Location = "https://dlcdnwebimgs.asus.com/gain/EA122891-9FE2-4ECF-AAAE-5DAC22E34B1E/w717/h525"},
                    new Image { Location = "https://dlcdnwebimgs.asus.com/gain/A385A525-C371-42CC-8A4C-07900772BFF6/w1000/h732"},
                },
                IsPublic = true,
                IsRemoved = false,
                Name = "ROG Strix GT15 G15",
                Price = 5000.25m,
                Psu = new Psu {Manufacturer = "Seasonic", Power = 500},
                PriceBeforeDiscount = 6000,
                Ram = new Ram 
                {
                    Manufacturer = "Corsair", 
                    Name = "LPX 100", 
                    FrequencyMHz = 3000, 
                    LatencyCL = 17, 
                    ModulesNumber = 2, 
                    RamTechnology = RamTechnologies.DDR4.ToString()
                },
            }
        };
                

        public async Task<Product?> GetProductByIdAsync(int id)
        {
            if(Products == null || Products.Count == 0)
            {
                await GetAllProductsAsync();
                if (Products == null || Products.Count == 0)
                {
                    return null;
                }                
            }
            return Products.FirstOrDefault(x => x.Id == id);
        }
        public async Task<List<Product>> GetAllProductsAsync()
        {
            return Products;
        }
        public async Task<List<Product>> GetProductsByCategoryIdAsync(int id)
        {
            return Products.Where(x => x.Category.Id == id.ToString()).ToList(); //tmp to string
        }
        public async Task<List<Product>> GetProductsByCategoryUrlAsync(string url)
        {
            return Products.Where(x => x.Category?.Name == url).ToList();
        }
    }
}
