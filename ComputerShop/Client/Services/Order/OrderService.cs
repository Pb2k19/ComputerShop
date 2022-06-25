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
        public async Task<ServiceResponse<OrderModel>?> AddOrderAsync(List<CartItem> cartItems, DeliveryDetails deliveryDetails, InvoiceDetails? invoiceDetails)
        {
            OrderInfo orderInfo = new()
            {
                CartItems = cartItems,
                DeliveryDetails = deliveryDetails,
                InvoiceDetails = invoiceDetails
            };
            using var response = await httpClient.PostAsJsonAsync("api/Order/addOrder", orderInfo);
            return await response.Content.ReadFromJsonAsync<ServiceResponse<OrderModel>>();
        }

        public async Task<ServiceResponse<List<OrderModel>>> GetAllOrdersForUserAsync()
        {
            return await httpClient.GetFromJsonAsync<ServiceResponse<List<OrderModel>>>($"api/Order/getAllOrdersForUser") 
                            ?? new ServiceResponse<List<OrderModel>>() { Success = false, Message="Nie można wczytać listy zamówień"};
        }
        public async Task<ServiceResponse<List<OrderModel>>> GetAllOrdersAsync()
        {
            return await httpClient.GetFromJsonAsync<ServiceResponse<List<OrderModel>>>($"api/Order/getAllOrders")
                            ?? new ServiceResponse<List<OrderModel>>() { Success = false, Message = "Nie można wczytać listy zamówień" };
        }
    }
}
