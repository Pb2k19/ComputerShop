using ComputerShop.Shared.Models;

namespace ComputerShop.Server.Services.Products
{
    public interface IProductsService
    {
        Task<Product?> GetProductByIdAsync(string id);
        Task<List<Product>> GetAllProductsAsync();
        Task<List<Product>> GetHighlightedProductsAsync();
        Task<List<Product>> GetProductsByIdListAsync(List<string> idList);
        Task<ProductsResponse> GetProductsByCategoryAsync(string category, int pageNumber = 1);
        Task<ProductsResponse> FindProductsByTextAsync(string text, int pageNumber = 1);
        Task<List<string>> GetProductsSuggestionsByTextAsync(string text);
    }
}
