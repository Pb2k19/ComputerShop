using ComputerShop.Shared.Models;
using ComputerShop.Shared.Models.Products;

namespace ComputerShop.Client.Services.PaymentService
{
    public interface IPaymentService
    {
        Task<ServiceResponse<string>> CreateCheckoutAsync(List<ProductCartItem> cartItems, string orderId);
    }
}