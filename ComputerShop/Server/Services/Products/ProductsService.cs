using ComputerShop.Server.DataAccess;
using ComputerShop.Server.Helpers;
using ComputerShop.Shared.Models;
using ComputerShop.Shared.Models.ComputerComponents;
using ComputerShop.Shared.Models.Products;
using MongoDB.Driver;

namespace ComputerShop.Server.Services.Products
{
    public class ProductsService : IProductsService
    {
        private ProductHelper productHelper = new();
        private IProductData productsData;

        public ProductsService(IProductData productsData)
        {
            this.productsData = productsData;
        }

        public async Task<Product?> GetProductByIdAsync(string id, bool isAdmin = false)
        {
            if(isAdmin)
                return await productsData.GetProductAsync(id);
            return await productsData.GetPublicProductAsync(id);
        }
        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await productsData.GetAllProductsAsync();
        }
        public async Task<List<Product>> GetHighlightedProductsAsync()
        {
            return (await productsData.GetAllPublicProductsAsync()).Where(p => p.IsHiglighted).ToList();
        }
        public async Task<ProductsResponse> GetProductsByCategoryAsync(string category, int pageNumber = 1, ProductSortFilterOptions? sortFilterOptions = null, bool isAdmin = false)
        {
            category = category.ToLower();
            List<Product>? prod = new();
            if (isAdmin)
                prod = await productsData.GetAllProductsAsync();
            else
                prod = await productsData.GetAllPublicProductsAsync();
            List<Product>? products = prod.Where(x => x.Category?.ToLower().Equals(category) ?? false).ToList();
            var manfucturers = GetManufacturers(products);
            if (sortFilterOptions != null)
                products = productHelper.SortFilterProducts(products, sortFilterOptions).ToList();            
            var pr = GetProductsResponse(products, pageNumber);
            pr.Manufacturers = manfucturers;
            return pr;
        }
        public async Task<ProductsResponse> FindProductsByTextAsync(string text, int pageNumber = 1, ProductSortFilterOptions? sortFilterOptions = null, bool isAdmin = false)
        {
            List<Product>? foundProducts = await FindProducts(text, isAdmin);
            var manfucturers = GetManufacturers(foundProducts);
            if (sortFilterOptions != null)
                foundProducts = productHelper.SortFilterProducts(foundProducts, sortFilterOptions).ToList();            
            var pr = GetProductsResponse(foundProducts, pageNumber);
            pr.Manufacturers = manfucturers;
            return pr;
        }
        public async Task<List<string>> GetProductsSuggestionsByTextAsync(string text)
        {
            text = text.ToLower();
            List<string> suggestions = new();
            List<Product> products = await FindProducts(text);
            if (products == null || products.Count == 0)
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
        private async Task<List<Product>> FindProducts(string text, bool isAdmin = false)
        {
            text = text.ToLower();
            List<string>? words = text.Split(' ').Distinct().ToList();
            List<Product> products = new();
            List<Product>? p = new();
            if (isAdmin)
                p = await productsData.GetAllProductsAsync();
            else
                p = await productsData.GetAllPublicProductsAsync();
            words.ForEach(word => products.AddRange(p
                .Where(x => (x.Manufacturer != null && x.Manufacturer.Contains(word, StringComparison.OrdinalIgnoreCase)) ||
                            (x.Name != null && x.Name.Contains(word, StringComparison.OrdinalIgnoreCase)) ||
                            (x.Description != null && x.Description.Contains(word, StringComparison.OrdinalIgnoreCase)))));
            return products.Distinct().ToList();
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
            else if (products == null)
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
        public async Task<List<Product>> GetProductsByIdListAsync(List<string> idList, bool isAdmin = false)
        {
            List<Product> products = new();
            foreach (var item in idList)
            {
                var prod = await GetProductByIdAsync(item, isAdmin);
                if (prod != null)
                    products.Add(prod);
            }
            return products;
        }
        public async Task<SimpleServiceResponse> AddProductAsync(string productJson, string category)
        {
            Product? newProduct;
            try
            {
                newProduct = productHelper.DeserializePorduct(category, productJson);
                if (newProduct is null)
                    return new SimpleServiceResponse() { Message = "Nie można dodać produktu", Success = false };
            }
            catch (Exception ex)
            {
                return new SimpleServiceResponse() { Message = ex.Message, Success = false };
            }
            if (newProduct.Images.Count == 0)
                newProduct.Images.Add(new Image() { Location = "images/default_image.png" });
            try
            {
                await productsData.AddProductAsync(newProduct);
                return new SimpleServiceResponse() { Success = true };
            }
            catch (MongoException ex)
            {
                return new SimpleServiceResponse() { Success = false, Message = ex.Message };
            }
        }
        public async Task<SimpleServiceResponse> AddCommentToProductAsync(Comment comment, string productId)
        {
            if (comment is null || string.IsNullOrEmpty(productId))
                return new SimpleServiceResponse() { Message = "Komentarz lub id produktu nie może mieć wartości null", Success = false };
            var product = await productsData.GetProductAsync(productId);
            if (product == null)
                return new SimpleServiceResponse() { Message = "Produkt nie może mieć wartości null", Success = false };
            product.Comments.Add(comment);
            await productsData.UpdateProductAsync(product);
            return new SimpleServiceResponse() { Success = true };
        }
        public async Task<SimpleServiceResponse> UpdateProductAsync(string productJson, string category)
        {
            Product? editProduct;
            try
            {
                editProduct = productHelper.DeserializePorduct(category, productJson);
                if (editProduct is null)
                    return new SimpleServiceResponse() { Message = "Nie można dodać produktu", Success = false };
            }
            catch (Exception ex)
            {
                return new SimpleServiceResponse() { Message = ex.Message, Success = false };
            }
            try
            {
                await productsData.UpdateProductAsync(editProduct);
                return new SimpleServiceResponse() { Success = true };
            }
            catch (MongoException ex)
            {
                return new SimpleServiceResponse() { Success = false, Message = ex.Message };
            }
        }
        public HashSet<string> GetManufacturers(List<Product> products)
        {
            return products.Select(p => p.Manufacturer).OrderBy(x => x).ToHashSet();
        }
    }
}