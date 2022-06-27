using ComputerShop.Shared.Models;
using ComputerShop.Shared.Models.Interfaces;
using ComputerShop.Shared.Models.Products;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace ComputerShop.Client.Services.Products
{
    public class ProductsService : IProductsService
    {
        private readonly HttpClient httpClient;

        public ProductsService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public async Task<ProductsResponse> GetAllByCategoryAsync(ProductSortFilterOptions sortFilterOptions, string category, int page)
        {
            using var response = await httpClient.PostAsJsonAsync($"api/products/getByCategory/{category}/{page}", sortFilterOptions);
            var result = await response.Content.ReadFromJsonAsync<ServiceResponse<ProductsResponse>>();
            if (result?.Success ?? false)
                return result?.Data ?? new ProductsResponse();
            return new ProductsResponse();
        }
        public async Task<List<Product>> GetAllAsync()
        {
            var response = await httpClient.GetFromJsonAsync<ServiceResponse<List<Product>>>("api/products/");
            if (response?.Success ?? false)
                return response?.Data ?? new List<Product>();
            return new List<Product>();
        }
        public async Task<List<Product>> GetAllHiglightedProductsAsync()
        {
            var response = await httpClient.GetFromJsonAsync<ServiceResponse<List<Product>>>("api/products/getHighlightedProduscts");
            if (response?.Success ?? false)
                return response?.Data ?? new List<Product>();
            return new List<Product>();
        }
        public async Task<Product?> GetProductByIdAsync(string id)
        {
            if(string.IsNullOrEmpty(id))
                return null;
            var response = await httpClient.GetFromJsonAsync<ServiceResponse<Product>>($"api/products/getProductById/{id}");
            if (response?.Success ?? false)
                return response?.Data;
            return null;
        }
        public async Task<T?> GetProductByIdAsync<T>(string id) where T : Product
        {
            var response = await httpClient.GetFromJsonAsync<ServiceResponse<T>>($"api/products/getProductById/{id}");
            if (response?.Success ?? false)
                return response?.Data;
            return null;
        }
        public async Task<ProductsResponse> FindByTextAsync(ProductSortFilterOptions sortFilterOptions, string text, int page)
        {
            using var response = await httpClient.PostAsJsonAsync($"api/products/find/{text}/{page}", sortFilterOptions);
            var result = await response.Content.ReadFromJsonAsync<ServiceResponse<ProductsResponse>>();
            if (result?.Success ?? false)
                return result?.Data ?? new ProductsResponse();
            return new ProductsResponse();
        }
        public async Task<List<string>> GetProductSuggestionsAsync(string text)
        {
            var response = await httpClient.GetFromJsonAsync<ServiceResponse<List<string>>>($"api/products/getProductSuggestions/{text}");
            if (response?.Success ?? false)
                return response?.Data ?? new List<string>();
            return new List<string>();
        }

        public async Task<List<Product>> GetProductsByIdListAsync(List<string> idList)
        {
            using var response = await httpClient.PostAsJsonAsync("api/products/getProductsByIdList", idList);
            var result = await response.Content.ReadFromJsonAsync<ServiceResponse<List<Product>>>();
            if(result?.Success ?? false)
                return result?.Data ?? new List<Product>();
            return new List<Product>();
        }

        public async Task<SimpleServiceResponse> EditProductAsync<T>(T? product) where T : IProduct
        {
            if(product == null)
                return new SimpleServiceResponse { Message = "Produkt nie może mieć wartości null", Success = false };

            StringValue productJson = new() { Value = JsonConvert.SerializeObject(product) };
            using var response = await httpClient.PostAsJsonAsync($"api/products/editProduct/{product.Category}", productJson);
            var result = await response.Content.ReadFromJsonAsync<SimpleServiceResponse>();
            if (result == null)
                return new SimpleServiceResponse { Message = "Coś poszło nie tak", Success = false };
            return result;
        }

        public async Task<SimpleServiceResponse> AddProductAsync<T>(T? product) where T : IProduct
        {
            if (product == null)
                return new SimpleServiceResponse { Message = "Produkt nie może mieć wartości null", Success = false };

            StringValue productJson = new() { Value = JsonConvert.SerializeObject(product) };
            using var response = await httpClient.PostAsJsonAsync($"api/products/addProduct/{product.Category}", productJson);
            var result = await response.Content.ReadFromJsonAsync<ServiceResponse<List<Product>>>();
            if (result == null)
                return new SimpleServiceResponse { Message = "Coś poszło nie tak", Success = false };
            return result;
        }

        public async Task<SimpleServiceResponse> AddCommentAsync(Comment product, string productId)
        {
            using var response = await httpClient.PostAsJsonAsync($"api/products/addComment/{productId}", product);
            var result = await response.Content.ReadFromJsonAsync<SimpleServiceResponse>();
            if (result == null)
                return new SimpleServiceResponse { Message = "Coś poszło nie tak", Success = false };
            return result;
        }
    }
}