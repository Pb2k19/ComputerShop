using ComputerShop.Server.DataAccess;
using ComputerShop.Shared.Models;
using ComputerShop.Shared.Models.Components;
using ComputerShop.Shared.Models.Products;

namespace ComputerShop.Server.Services.Products
{
    public class ProductsService : IProductsService
    {
        private IProductData productsData;

        public ProductsService(IProductData productsData)
        {
            this.productsData = productsData;
        }

        public async Task<Product?> GetProductByIdAsync(string id)
        {
            return await productsData.GetProductAsync(id);
        }
        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await productsData.GetAllProductsAsync();
        }
        public async Task<List<Product>> GetHighlightedProductsAsync()
        {
            return (await productsData.GetAllPublicProductsAsync()).Where(p => p.IsHiglighted).ToList();
        }
        public async Task<ProductsResponse> GetProductsByCategoryAsync(string category, int pageNumber = 1)
        {
            category = category.ToLower();
            var prod = await productsData.GetAllPublicProductsAsync();
            List<Product>? products = prod.Where(x => x.Category?.ToLower().Equals(category) ?? false).ToList();
            return GetProductsResponse(products, pageNumber);
        }
        public async Task<ProductsResponse> FindProductsByTextAsync(string text, int pageNumber = 1)
        {
            List<Product>? foundProducts = await FindProducts(text);
            return GetProductsResponse(foundProducts, pageNumber);
        }
        public async Task<List<string>> GetProductsSuggestionsByTextAsync(string text)
        {
            text = text.ToLower();
            List<string> suggestions = new();
            List<Product> products = await FindProducts(text);
            if(products == null || products.Count == 0)
            {
                return suggestions;
            }

            suggestions.AddRange(products
                .Select(p => p.Name)?
                .Where(s => s != null && s.Contains(text, StringComparison.OrdinalIgnoreCase))
                .ToList() ?? new List<string>());

            suggestions.AddRange(products
                .Select(p => p.Manufacturer)?
                .Where(s => s != null && s.Contains(text, StringComparison.OrdinalIgnoreCase))
                .ToList() ?? new List<string>());

            return suggestions.Distinct().ToList();
        }

        private async Task<List<Product>> FindProducts(string text)
        {
            text = text.ToLower();
            List<string>? words = text.Split(' ').ToList();
            List<Product> products = new();
            var p = await productsData.GetAllPublicProductsAsync();
            words.ForEach(word => products.AddRange(p
                .Where(x => (x.Manufacturer != null && x.Manufacturer.Contains(word, StringComparison.OrdinalIgnoreCase)) ||
                            (x.Name != null && x.Name.Contains(word, StringComparison.OrdinalIgnoreCase)) ||
                            (x.Description != null && x.Description.Contains(word, StringComparison.OrdinalIgnoreCase)))));
            return products;
        }

        private ProductsResponse GetProductsResponse(List<Product> products, int pageNumber)
        {
            if (pageNumber < 1)
                return new ProductsResponse() { };
            int maxProductsOnPage = 12;
            int pageCount = 0;
            if (products != null && products.Count > 0)
            {
                float x = products.Count / (float)maxProductsOnPage;
                pageCount = (int)Math.Ceiling(x);
                products = products.Skip((pageNumber - 1) * maxProductsOnPage).Take(maxProductsOnPage).ToList();
            }
            else if(products == null)
            {
                products = new List<Product>();
            }
            return new ProductsResponse()
            {
                Products = products,
                CurrentPage = pageNumber,
                PagesCount = pageCount
            };
        }

        public async Task<List<Product>> GetProductsByIdListAsync(List<string> idList)
        {
            List<Product> products = new();
            foreach (var item in idList)
            {
                var prod = await GetProductByIdAsync(item);
                if (prod != null)
                    products.Add(prod);
            }
            return products;
        }
    }
}
