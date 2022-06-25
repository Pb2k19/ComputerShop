using ComputerShop.Shared.Models;
using ComputerShop.Shared.Models.User;

namespace ComputerShop.Server.Services.Order
{
    public interface IOrderService
    {
        Task<ServiceResponse<OrderModel>> AddOrderAsync(List<CartItem> cartItems, DeliveryDetails deliveryDetails, InvoiceDetails invoiceDetails, bool isAuthenticated);
        Task<ServiceResponse<OrderModel>> GetOrderAsync(string orderId);
        Task<SimpleServiceResponse> UpdateOrderAsync(OrderModel order);
        Task<ServiceResponse<List<OrderModel>>> GetAllOrdersForUserAsync();
        Task<ServiceResponse<List<OrderModel>>> GetAllOrdersAsync();
    }
}
