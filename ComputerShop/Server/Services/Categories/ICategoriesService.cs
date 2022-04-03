using ComputerShop.Shared.Models;

namespace ComputerShop.Server.Services.Categories
{
    public interface ICategoriesService
    {
        List<Category> Categories { get; set; }
        Task<List<Category>> GetCategoriesAsync();
        Task<Category?> GetCategoryByIdAsync(string id);
        Task<Category?> GetCategoryByUrlAsync(string url);
    }
}
