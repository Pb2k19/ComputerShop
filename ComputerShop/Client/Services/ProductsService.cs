using ComputerShop.Shared.Models;
using ComputerShop.Shared.Models.Products;
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
        public async Task LoadAsync(int id)
        {
            Products = await httpClient.GetFromJsonAsync<List<Product>>($"api/products/getByCategoryId/{id}") ?? new List<Product>();
        }
        public async Task LoadAsync()
        {
            Products = await httpClient.GetFromJsonAsync<List<Product>>($"api/products/") ?? new List<Product>();
        }
        public async Task<Product?> GetProductById(int id)
        {
            return await httpClient.GetFromJsonAsync<DesktopPcProduct>($"api/products/getProductById/{id}");
        }
        public async Task<T?> GetProductGeneric<T>(int id) where T : Product
        {
            return await httpClient.GetFromJsonAsync<T>($"api/products/getProductById/{id}");
        }
    }
}
