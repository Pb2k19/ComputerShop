using ComputerShop.Shared.Models;

namespace ComputerShop.Server.Services
{
    public interface IProductsService
    {
        List<Product> Products { get; set; }
        Task<Product?> GetProductByIdAsync(int id);
        Task<List<Product>> GetAllProductsAsync();
        Task<List<Product>> GetProductsByCategoryUrlAsync(string url);
        Task<List<Product>> GetProductsByCategoryIdAsync(int id);
    }
}
