using ComputerShop.Shared.Models;
using ComputerShop.Shared.Models.Products;

namespace ComputerShop.Server.Services
{
    public class CategoriesService : ICategoriesService
    {
        public List<Category> Categories { get; set; } = new();
        public async Task<List<Category>> GetCategoriesAsync()
        {
            await Task.Run(() =>
            {
                Categories = new List<Category>()
                {
                    new LaptopProduct().Category,
                    new DesktopPcProduct().Category,
                    new DesktopGpuProduct().Category,
                    new DesktopPsuProduct().Category,
                    new MotherboardProduct().Category,
                    new RamProduct().Category,
                    new CpuProduct().Category,
                    new SsdProduct().Category,
                    new HddProduct().Category,
                    new DesktopCaseProduct().Category,
                    new ComputerMouseProduct().Category,
                    new KeyboardProduct().Category,
                    new HeadphonesProduct().Category,
                    new MonitorProduct().Category,
                    new PrinterProduct().Category,
                    new CableProduct().Category,
                    new ToolProduct().Category,
                    new DesktopCoolerProduct().Category,
                };
            });
            return Categories;
        }

        public async Task<Category?> GetCategoryByIdAsync(int id)
        {
            if(Categories == null || Categories.Count == 0)
            {
                await GetCategoriesAsync();
                if (Categories == null || Categories.Count == 0)
                {
                    return null;
                }
            }
            return Categories.FirstOrDefault(x => x.Id == id.ToString()); //tmp to string
        }

        public async Task<Category?> GetCategoryByUrlAsync(string url)
        {
            if (Categories == null || Categories.Count == 0)
            {
                await GetCategoriesAsync();
                if (Categories == null || Categories.Count == 0)
                {
                    return null;
                }
            }
            return Categories.FirstOrDefault(x => x.Url.ToUpper() == url.ToUpper());
        }
    }
}
