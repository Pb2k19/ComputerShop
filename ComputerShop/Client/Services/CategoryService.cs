using ComputerShop.Shared.Models;

namespace ComputerShop.Client.Services
{
    public class CategoryService : ICategoryService
    {
        public List<Category> Categories { get; set; } = new();

        public void Load()
        {
            Categories = new()
            {
                new Category { Id = 1, Name = "Computers", Url = "computers", Icon = "book" },
                new Category { Id = 2, Name = "Smartphones", Url = "smartphones", Icon = "aperture" },
                new Category { Id = 3, Name = "GPUs", Url = "gpus", Icon = "camera-slr" },
            };
        }
    }
}
