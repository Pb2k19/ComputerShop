using Stripe;
using Stripe.Checkout;
using ComputerShop.Server.Services.Order;
using ComputerShop.Server.Services.User;
using ComputerShop.Shared.Models.User;
using ComputerShop.Server.Services.Products;
using ComputerShop.Shared.Models;

namespace ComputerShop.Server.Services.Payment
{
    public class PaymentService : IPaymentService
    {
        private readonly IOrderService orderService;
        private readonly IProductsService productsService;
        private readonly IUserService userService;
        private readonly string siteUrl;
        private readonly string stripeSecret;

        public PaymentService(IConfiguration configuration, IOrderService orderService, IProductsService productsService, IUserService userService)
        {            
            this.orderService = orderService;
            this.productsService = productsService;
            this.userService = userService;
            StripeConfiguration.ApiKey = configuration["Settings:StripeApiKey"];
            siteUrl = configuration["Settings:PageUrl"];
            stripeSecret = configuration["Settings:StripeSecret"];
        }
        public Session CreateCheckout(List<ProductCartItem> products, string email)
        {
            List<SessionLineItemOptions> items = new();
            products.ForEach(p => items.
                Add(new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        Currency = "pln",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Images = new List<string> { p.Product.Images.FirstOrDefault()?.Location ?? new Image().Location },
                            Name = $"{p.Product.Manufacturer} {p.Product.Name}",
                        },
                        UnitAmountDecimal = p.Product.Price * 100,
                    },
                    Quantity = p.Quantity
                }));

            SessionCreateOptions options = new()
            {
                CustomerEmail = email,
                CancelUrl = $"{siteUrl}/card",
                SuccessUrl = $"{siteUrl}/sucess-order",
                Mode = "payment",
            };

            SessionService sessionService = new();
            return sessionService.Create(options);
        }

        public async Task<SimpleServiceResponse> FulfillOrder(HttpRequest request, string orderId)
        {
            string requestBody = await new StreamReader(request.Body).ReadToEndAsync();
            string stripeSingature = request.Headers["Stripe-Signature"];
            try
            {
                Event stripeEvent = EventUtility.ConstructEvent(requestBody, stripeSingature, stripeSecret);
                if(stripeEvent.Type.Equals(Events.CheckoutSessionCompleted))
                {
                    Session session = (Session)stripeEvent.Data.Object;
                    var orderResponse = await orderService.GetOrderAsync(orderId);
                    if(!orderResponse.Success)
                        return new SimpleServiceResponse { Message = orderResponse.Message, Success = false };
                    if (orderResponse.Data is null)
                        return new SimpleServiceResponse { Message = "Coś poszło nie tak", Success = false };
                    var order = orderResponse.Data;
                    order.State = OrderStates.InPreparation;
                    await orderService.UpdateOrderAsync(order);
                }
                return new SimpleServiceResponse { Success = true };
            }
            catch (StripeException ex)
            {
                return new SimpleServiceResponse { Message = ex.Message, Success = false };
            }
        }
    }
}
