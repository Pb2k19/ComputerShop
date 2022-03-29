using ComputerShop.Shared.Models;
using System.Net.Http.Json;

namespace ComputerShop.Client.Services
{
    public class ProductsService : IProductsService
    {
        private readonly HttpClient httpClient;
        public List<Product> Products { get; set; } = new List<Product>();

        public ProductsService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public async Task<ProductsResponse> LoadByCategoryIdAsync(string id, int page)
        {
            var response = await httpClient.GetFromJsonAsync<ServiceResponse<ProductsResponse>>($"api/products/getByCategoryId/{id}/{page}");
            Products = response?.Data?.Products ?? new List<Product>();
            return response?.Data ?? new ProductsResponse();
        }
        public async Task LoadAllAsync()
        {
            var response = await httpClient.GetFromJsonAsync<ServiceResponse<List<Product>>>($"api/products/");
            Products = response?.Data ?? new List<Product>();
        }
        public async Task<Product?> GetProductByIdAsync(string id)
        {
            if(string.IsNullOrEmpty(id))
                return null;
            var response = await httpClient.GetFromJsonAsync<ServiceResponse<Product>>($"api/products/getProductById/{id}");
            return response?.Data;
        }
        public async Task<T?> GetProductByIdAsync<T>(string id) where T : Product
        {
            var response = await httpClient.GetFromJsonAsync<ServiceResponse<T>>($"api/products/getProductById/{id}");
            return response?.Data;
        }
        public async Task LoadByTextAsync(string text)
        {
            var response = await httpClient.GetFromJsonAsync<ServiceResponse<List<Product>>>($"api/products/find/{text}");
            Products = response?.Data ?? new List<Product>();
        }
        public async Task<List<string>> GetProductSuggestionsAsync(string text)
        {
            var response = await httpClient.GetFromJsonAsync<ServiceResponse<List<string>>>($"api/products/getProductSuggestions/{text}");
            return response?.Data ?? new List<string>();
        }

        public async Task<List<Product>> GetProductsByIdListAsync(List<string> idList)
        {
            using var response = await httpClient.PostAsJsonAsync("api/products/getProductsByIdList", idList);
            var result = await response.Content.ReadFromJsonAsync<ServiceResponse<List<Product>>>();
            return result?.Data ?? new List<Product>();
        }
    }
}