using ComputerShop.Shared.Models;

namespace ComputerShop.Client.Services.Products
{
    public interface IProductsService
    {
        Task<List<Product>> GetAllAsync();
        Task<ProductsResponse> GetAllByCategoryAsync(ProductSortFilterOptions sortFilterOptions, string category, int page);
        Task<List<Product>> GetAllHiglightedProductsAsync();
        Task<ProductsResponse> FindByTextAsync(ProductSortFilterOptions sortFilterOptions, string text, int page);
        Task<Product?> GetProductByIdAsync(string id);
        Task<List<Product>> GetProductsByIdListAsync(List<string> id);
        Task<T?> GetProductByIdAsync<T>(string id) where T : Product;
        Task<List<string>> GetProductSuggestionsAsync(string text);
    }
}