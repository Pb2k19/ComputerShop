using ComputerShop.Shared.Models;

namespace ComputerShop.Server.Services.Products
{
    public interface IProductsService
    {
        List<Product> Products { get; set; }
        Task<Product?> GetProductByIdAsync(string id);
        Task<List<Product>> GetAllProductsAsync();
        Task<List<Product>> GetHiglightedProductsAsync();
        Task<ProductsResponse> GetProductsByCategoryUrlAsync(string url, int pageNumber = 1);
        Task<ProductsResponse> GetProductsByCategoryIdAsync(string id, int pageNumber = 1);
        Task<ProductsResponse> FindProductsByTextAsync(string text, int pageNumber = 1);
        Task<List<string>> GetProductsSuggestionsByTextAsync(string text);
    }
}
