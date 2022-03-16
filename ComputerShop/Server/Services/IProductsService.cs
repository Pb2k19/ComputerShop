using ComputerShop.Shared.Models;

namespace ComputerShop.Server.Services
{
    public interface IProductsService
    {
        List<Product> Products { get; set; }
        Task<Product?> GetProductByIdAsync(string id);
        Task<List<Product>> GetAllProductsAsync();
        Task<List<Product>> GetHiglightedProductsAsync();
        Task<ProductsResponse> GetProductsByCategoryUrlAsync(string url, int pageNumber);
        Task<ProductsResponse> GetProductsByCategoryIdAsync(string id, int pageNumber);
        Task<ProductsResponse> FindProductsByTextAsync(string text, int pageNumber);
        Task<List<string>> GetProductsSuggestionsByTextAsync(string text);
    }
}
