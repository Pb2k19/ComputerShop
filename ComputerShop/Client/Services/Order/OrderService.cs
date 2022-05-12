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
            if (invoiceDetails is InvoiceDetailsForBusiness invoiceDetailsForBusiness)
            {
                OrderInfoForBussiness orderInfoForBussiness = new()
                {
                    CartItems = cartItems,
                    DeliveryDetails = deliveryDetails,
                    InvoiceDetails = invoiceDetailsForBusiness
                };
                using var response = await httpClient.PostAsJsonAsync("api/Order/addOrderForBusiness", orderInfoForBussiness);
                return await response.Content.ReadFromJsonAsync<ServiceResponse<OrderModel>>();
            }
            else
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
        }
    }
}
