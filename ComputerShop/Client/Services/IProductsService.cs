using ComputerShop.Shared.Models;

namespace ComputerShop.Client.Services
{
    public interface IProductsService
    {
        List<Product> Products { get; set; }
        Task LoadAllAsync();
        Task<ProductsResponse> LoadByCategoryIdAsync(string id, int page);
        Task LoadByTextAsync(string text);
        Task<Product?> GetProductByIdAsync(string id);
        Task<List<Product>> GetProductsByIdListAsync(List<string> id);
        Task<T?> GetProductByIdAsync<T>(string id) where T : Product;
        Task<List<string>> GetProductSuggestionsAsync(string text);
    }
}