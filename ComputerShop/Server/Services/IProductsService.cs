using ComputerShop.Shared.Models;

namespace ComputerShop.Server.Services
{
    public interface IProductsService
    {
        List<Product> Products { get; set; }
        Task<Product?> GetProductByIdAsync(string id);
        Task<List<Product>> GetAllProductsAsync();
        Task<List<Product>> GetProductsByCategoryUrlAsync(string url);
        Task<List<Product>> GetProductsByCategoryIdAsync(string id);
        Task<List<Product>> FindProductsByTextAsync(string text);
        Task<List<string>> GetProductsSuggestionsByTextAsync(string text);
    }
}
