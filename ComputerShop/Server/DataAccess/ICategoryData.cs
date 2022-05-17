using ComputerShop.Shared.Models;

namespace ComputerShop.Server.DataAccess
{
    public interface ICategoryData
    {
        Task<List<Category>> GetAllCategoriesAsync();
        Task AddCategoryAsync(Category category);
    }
}