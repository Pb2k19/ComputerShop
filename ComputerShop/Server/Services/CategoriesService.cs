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
                    new Category { Id = "1", Name = "Computers", Url = "computers", Icon = "fas fa-desktop" },
                    new Category { Id = "2", Name = "Smartphones", Url = "smartphones", Icon = "fas fa-mobile-alt" },
                    new Category { Id = "3", Name = "GPUs", Url = "gpus", Icon = "fas fa-server" },
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
