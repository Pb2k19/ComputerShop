using ComputerShop.Shared.Models;

namespace ComputerShop.Server.DataAccess
{
    public interface IProductData
    {
        Task AddProductAsync(Product product);
        Task<List<Product>> GetAllHiddenProductsAsync();
        Task<List<Product>> GetAllProductsAsync();
        Task<List<Product>> GetAllPublicProductsAsync();
        Task<List<Product>> GetAllRemovedProductsAsync();
        Task<Product> GetProductAsync(string id);
        Task<Product> GetPublicProductAsync(string id);
        Task UpdateProductAsync(Product product);
    }
}