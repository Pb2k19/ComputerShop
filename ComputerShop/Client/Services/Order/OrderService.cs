using System.Net.Http.Json;
using ComputerShop.Shared.Models;
using ComputerShop.Shared.Models.User;

namespace ComputerShop.Client.Services.Order
{
    public class OrderService : IOrderService
    {
        private readonly HttpClient httpClient;

        public OrderService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public async Task<ServiceResponse<OrderModel>?> AddOrderAsync(OrderInfo info)
        {
            using var response = await httpClient.PostAsJsonAsync("addOrder", info);
            return await response.Content.ReadFromJsonAsync<ServiceResponse<OrderModel>>();
        }
    }
}
