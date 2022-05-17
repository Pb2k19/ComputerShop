using ComputerShop.Shared.Models;
using Microsoft.Extensions.Caching.Memory;
using MongoDB.Driver;

namespace ComputerShop.Server.DataAccess
{
    public class CategoryData : ICategoryData
    {
        private readonly IMemoryCache memoryCache;
        private readonly IMongoCollection<Category> categories;
        private const string CacheKey = "CategoryDataCache";

        public CategoryData(IDbConnection dbConnection, IMemoryCache memoryCache)
        {
            this.memoryCache = memoryCache;
            categories = dbConnection.CategoryCollection;
        }
        public async Task<List<Category>> GetAllCategoriesAsync()
        {
            var result = memoryCache.Get<List<Category>>(CacheKey);
            if (categories == null)
            {
                result = (await categories.FindAsync(_ => true)).ToList();
                memoryCache.Set(CacheKey, result, TimeSpan.FromDays(1));
            }
            return result;
        }
        public Task AddCategoryAsync(Category category)
        {
            return categories.InsertOneAsync(category);
        }
    }
}
