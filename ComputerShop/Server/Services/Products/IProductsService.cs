using ComputerShop.Shared.Models;
using ComputerShop.Shared.Models.Products;

namespace ComputerShop.Server.Services.Products
{
    public interface IProductsService
    {
        Task<Product?> GetProductByIdAsync(string id);
        Task<List<Product>> GetAllProductsAsync();
        Task<List<Product>> GetHighlightedProductsAsync();
        Task<List<Product>> GetProductsByIdListAsync(List<string> idList);
        Task<ProductsResponse> GetProductsByCategoryAsync(string category, int pageNumber = 1, ProductSortFilterOptions? sortFilterOptions = null);
        Task<ProductsResponse> FindProductsByTextAsync(string text, int pageNumber = 1, ProductSortFilterOptions? sortFilterOptions = null);
        Task<List<string>> GetProductsSuggestionsByTextAsync(string text);
        Task<SimpleServiceResponse> AddProductAsync(Product product);
        Task<SimpleServiceResponse> AddCommentToProductAsync(Comment comment, string productId);
        Task<SimpleServiceResponse> UpdateProductAsync(Product product);
    }
}
