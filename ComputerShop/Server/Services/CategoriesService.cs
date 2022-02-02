using ComputerShop.Shared.Models;

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
                    new Category { Id = 1, Name = "Computers", Url = "computers", Icon = "book" },
                    new Category { Id = 2, Name = "Smartphones", Url = "smartphones", Icon = "aperture" },
                    new Category { Id = 3, Name = "GPUs", Url = "gpus", Icon = "camera-slr" },
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
            return Categories.FirstOrDefault(x => x.Id == id);
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
