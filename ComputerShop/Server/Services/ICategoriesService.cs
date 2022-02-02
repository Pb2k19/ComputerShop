using ComputerShop.Shared.Models;

namespace ComputerShop.Server.Services
{
    public interface ICategoriesService
    {
        List<Category> Categories { get; set; }
        Task<List<Category>> GetCategoriesAsync();
        Task<Category?> GetCategoryByIdAsync(int id);
        Task<Category?> GetCategoryByUrlAsync(string url);
    }
}
