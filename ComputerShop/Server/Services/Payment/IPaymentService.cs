using Stripe.Checkout;
using ComputerShop.Shared.Models.Products;
using ComputerShop.Shared.Models;

namespace ComputerShop.Server.Services.Payment
{
    public interface IPaymentService
    {
        Session CreateCheckout(List<ProductCartItem> products, string email);
        Task<SimpleServiceResponse> FulfillOrder(HttpRequest request);
    }
}