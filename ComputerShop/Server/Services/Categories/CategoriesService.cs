﻿using ComputerShop.Server.DataAccess;
using ComputerShop.Shared.Models;
using ComputerShop.Shared.Models.Products;

namespace ComputerShop.Server.Services.Categories
{
    public class CategoriesService : ICategoriesService
    {
        private readonly ICategoryData categoryData;

        public List<Category> Categories { get; set; } = new();
        public async Task<List<Category>> GetCategoriesAsync()
        {
            await Task.Run(async () =>
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

        public CategoriesService(ICategoryData categoryData)
        {
            this.categoryData = categoryData;
        }
        public async Task<Category?> GetCategoryByIdAsync(string id)
        {
            if(Categories == null || Categories.Count == 0)
            {
                await GetCategoriesAsync();
                if (Categories == null || Categories.Count == 0)
                {
                    return null;
                }
            }
            return Categories.FirstOrDefault(x => x.Id != null && x.Id.Equals(id));
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
