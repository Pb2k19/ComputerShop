using ComputerShop.Shared.Models.Products;
using Microsoft.Extensions.Caching.Memory;
using MongoDB.Driver;

namespace ComputerShop.Server.DataAccess
{
    public class ProductData : IProductData
    {
        private readonly IMongoCollection<Product> products;
        private readonly IMemoryCache memoryCache;
        private const string CacheKey = "ProductDataCache";
        public ProductData(IDbConnection dbConnection, IMemoryCache memoryCache)
        {
            this.memoryCache = memoryCache;
            products = dbConnection.ProductCollection;
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            var result = memoryCache.Get<List<Product>>(CacheKey);
            if (result == null)
            {
                result = (await products.FindAsync(_ => true)).ToList();
                memoryCache.Set(CacheKey, result, TimeSpan.FromMinutes(2));
            }
            return result;
        }
        public async Task<List<Product>> GetAllPublicProductsAsync()
        {
            return (await GetAllProductsAsync()).Where(p => p.IsPublic && !p.IsRemoved).ToList();
        }
        public async Task<List<Product>> GetAllHiddenProductsAsync()
        {
            return (await GetAllProductsAsync()).Where(p => !p.IsPublic).ToList();
        }
        public async Task<List<Product>> GetAllRemovedProductsAsync()
        {
            return (await GetAllProductsAsync()).Where(p => p.IsRemoved).ToList();
        }
        public async Task<Product> GetProductAsync(string id)
        {
            return await (await products.FindAsync(p => p.Id.Equals(id))).FirstOrDefaultAsync();
        }
        public async Task<Product> GetPublicProductAsync(string id)
        {
            return await (await products.FindAsync(p => p.Id.Equals(id) && p.IsPublic)).FirstOrDefaultAsync();
        }
        public Task AddProductAsync(Product product)
        {
            product.CareationDate = product.LastUpdateDate = DateTime.UtcNow;
            return products.InsertOneAsync(product);
        }
        public async Task UpdateProductAsync(Product product)
        {
            product.LastUpdateDate = DateTime.UtcNow;
            await products.ReplaceOneAsync(p => p.Id.Equals(product.Id), product);
            memoryCache.Remove(CacheKey);
        }
    }
}
