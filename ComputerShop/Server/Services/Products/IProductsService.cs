using ComputerShop.Shared.Models;
using ComputerShop.Shared.Models.Products;

namespace ComputerShop.Server.Services.Products
{
    public interface IProductsService
    {
        Task<Product?> GetProductByIdAsync(string id, bool isAdmin = false);
        Task<List<Product>> GetAllProductsAsync();
        Task<List<Product>> GetHighlightedProductsAsync();
        Task<List<Product>> GetProductsByIdListAsync(List<string> idList, bool isAdmin = false);
        Task<ProductsResponse> GetProductsByCategoryAsync(string category, int pageNumber = 1, ProductSortFilterOptions? sortFilterOptions = null, bool isAdmin = false);
        Task<ProductsResponse> FindProductsByTextAsync(string text, int pageNumber = 1, ProductSortFilterOptions? sortFilterOptions = null, bool isAdmin = false);
        Task<List<string>> GetProductsSuggestionsByTextAsync(string text);
        Task<SimpleServiceResponse> AddProductAsync(string productJson, string category);
        Task<SimpleServiceResponse> AddCommentToProductAsync(Comment comment, string productId);
        Task<SimpleServiceResponse> UpdateProductAsync(string productJson, string category);
    }
}
