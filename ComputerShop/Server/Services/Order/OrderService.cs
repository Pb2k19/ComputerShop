using ComputerShop.Server.Services.User;
using ComputerShop.Shared.Models;
using ComputerShop.Shared.Models.User;
using ComputerShop.Server.Services.Products;

namespace ComputerShop.Server.Services.Order
{
    public class OrderService : IOrderService
    {
        private readonly IProductsService productsService;
        private readonly IUserService userService;
        public OrderService(IProductsService productsService, IUserService userService)
        {
            this.productsService = productsService;
            this.userService = userService;
        }

        public async Task<ServiceResponse<OrderModel>> AddOrderAsync(List<CartItem> cartItems, DeliveryDetails deliveryDetails, InvoiceDetails invoiceDetails)
        {
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

            OrderModel order = new() { CartItems = cartItems, State = "Nowe", Id = "1", Total = cartSum, DeliveryDetails = deliveryDetails, InvoiceDetails = invoiceDetails };
            return new ServiceResponse<OrderModel> { Success = true, Data = order };
        }

        public async Task<ServiceResponse<List<OrderModel>>> GetAllOrdersForUser()
        {
            string? userId = userService.GetUserId();
            if (userId == null)
                return new ServiceResponse<List<OrderModel>> { Message = "Coś poszło nie tak", Success = false };
            UserModel? user = await userService.GetUserByIdAsync(userId);
            if (user == null)
                return new ServiceResponse<List<OrderModel>> { Message = "Coś poszło nie tak", Success = false };
            return new ServiceResponse<List<OrderModel>> { Data = user.Orders };
        }
    }
}
