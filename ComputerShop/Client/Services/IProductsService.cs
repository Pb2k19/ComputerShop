using ComputerShop.Shared.Models;

namespace ComputerShop.Client.Services
{
    public interface IProductsService
    {
        List<Product> Products { get; set; }
        Task LoadAllAsync();
        Task LoadByCategoryIdAsync(string id);
        Task LoadByTextAsync(string text);
        Task<Product?> GetProductByIdAsync(int id);
        Task<T?> GetProductByIdAsync<T>(int id) where T : Product;
        Task<List<string>> GetProductSuggestionsAsync(string text);
    }
}