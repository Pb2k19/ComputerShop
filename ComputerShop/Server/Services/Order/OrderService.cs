using ComputerShop.Client.Services.User;
using ComputerShop.Server.Services.Products;
using ComputerShop.Shared.Models;
using ComputerShop.Shared.Models.User;

namespace ComputerShop.Server.Services.Order
{
    public class OrderService : IOrderService
    {
        private readonly IProductsService productsService;

        public OrderService()
        {
            productsService = new ProductsService();
        }

        public async Task<ServiceResponse<OrderModel>> AddOrderAsync(List<CartItem> cartItems, DeliveryDetails deliveryDetails)
        {
            if(!deliveryDetails.QuickValidate())
                return new ServiceResponse<OrderModel> { Success = false, Message = "Dane wysyłki nie są prawidłowe" };

            var products = await productsService.GetProductsByIdListAsync(cartItems.Select(p => p.ProductId).ToList());
            List<ProductCartItem> items = new();
            products.ForEach(
                p => items.Add(new ProductCartItem
                {
                    Product = p,
                    Quantity = cartItems.FirstOrDefault(c => c.ProductId.Equals(p.Id))?.Quantity ?? 1
                }));

            decimal cartSum = 0, productsSum = 0;
            cartItems.ForEach(i => cartSum += i.Price * i.Quantity);
            items.ForEach(p => productsSum += p.Product.Price * p.Quantity);

            if (cartSum != productsSum)
                return new ServiceResponse<OrderModel> { Success = false, Message = "Cena uległa zmianie" };

            OrderModel order = new() { CartItems = cartItems, State = "Nowe", Id = "1", Total = cartSum, DeliveryDetails = deliveryDetails };
            return new ServiceResponse<OrderModel> { Success = true, Data = order };
        }
    }
}
