using ComputerShop.Shared.Models;
using System.Net.Http.Json;

namespace ComputerShop.Client.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly HttpClient httpClient;
        public List<Category> Categories { get; set; } = new();

        public CategoryService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task LoadAsync()
        {
            Categories = await httpClient.GetFromJsonAsync<List<Category>>("api/categories") ?? new List<Category>();
        }

        public async Task<Category?> GetCategoryById(int categoryId)
        {
            return await httpClient.GetFromJsonAsync<Category>($"api/categories/getCategoryById/{categoryId}");
        }

        public async Task<Category?> GetCategoryByUrl(string categoryUrl)
        {
            return await httpClient.GetFromJsonAsync<Category>($"api/categories/getCategoryByUrl/{categoryUrl}");
        }
    }
}