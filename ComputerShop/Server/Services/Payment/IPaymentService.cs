using Stripe.Checkout;
using ComputerShop.Shared.Models.Products;

namespace ComputerShop.Server.Services.Payment
{
    public interface IPaymentService
    {
        Session CreateCheckout(List<ProductCartItem> products, string email);
    }
}