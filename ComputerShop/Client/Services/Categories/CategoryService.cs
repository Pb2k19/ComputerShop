using ComputerShop.Shared.Models;
using System.Net.Http.Json;
using System.Net;

namespace ComputerShop.Client.Services.Categories
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
            try
            {
                return await httpClient.GetFromJsonAsync<Category>($"api/categories/getCategoryByUrl/{categoryUrl}");
            }
            catch (HttpRequestException ex)
            {
                if (ex.StatusCode == HttpStatusCode.NotFound)
                    return null;
                throw;
            }
        }
    }
}