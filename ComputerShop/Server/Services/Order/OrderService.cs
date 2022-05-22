using ComputerShop.Server.Services.User;
using ComputerShop.Shared.Models;
using ComputerShop.Shared.Models.User;
using ComputerShop.Server.Services.Products;
using ComputerShop.Server.DataAccess;

namespace ComputerShop.Server.Services.Order
{
    public class OrderService : IOrderService
    {
        private readonly IProductsService productsService;
        private readonly IUserService userService;
        private readonly IUserData userData;
        public OrderService(IProductsService productsService, IUserService userService, IUserData userData)
        {
            this.productsService = productsService;
            this.userService = userService;
            this.userData = userData;
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

            OrderModel order = new() { CartItems = cartItems, State = OrderStates.Unpaid, Total = cartSum, DeliveryDetails = deliveryDetails, InvoiceDetails = invoiceDetails };
            UserModel? user;

            string? userId = userService.GetUserId();
            if(!string.IsNullOrEmpty(userId))
            {
                user = await userService.GetUserByIdAsync(userId);
                if(user != null)
                {
                    user.Orders.Add(order);
                    var response = await userService.UpdateUserAsync(user);
                    if(response.Success)
                        return new ServiceResponse<OrderModel> { Success = true, Data = order };
                    else
                        return new ServiceResponse<OrderModel> { Success = false, Data = order };
                }
            }
            user = new UserModel { Orders = new() { order } }; //tmp
            await userService.AddUserAsync(user);

            return new ServiceResponse<OrderModel> { Success = true, Data = order };
        }

        public async Task<ServiceResponse<List<OrderModel>>> GetAllOrdersForUser()
        {
            string? userId = userService.GetUserId();
            if (string.IsNullOrWhiteSpace(userId))
                return new ServiceResponse<List<OrderModel>> { Message = "Coś poszło nie tak", Success = false };
            UserModel? user = await userService.GetUserByIdAsync(userId);
            if (user == null)
                return new ServiceResponse<List<OrderModel>> { Message = "Coś poszło nie tak", Success = false };
            return new ServiceResponse<List<OrderModel>> { Data = user.Orders };
        }

        public async Task<ServiceResponse<OrderModel>> GetOrderAsync(string orderId)
        {
            try
            {
                string? userId = userService.GetUserId();
                OrderModel? order;
                if (!string.IsNullOrWhiteSpace(userId))
                    order = await userData.GetOrderAsync(orderId, userId);
                else
                    order = await userData.GetOrderAsync(orderId);
                if (order != null)
                    return new ServiceResponse<OrderModel> { Data = order, Success = true };
                else
                    return new ServiceResponse<OrderModel> { Success = false, Message = "Nie znaleziono zamówienia" };
            }
            catch (MongoDB.Driver.MongoException ex)
            {
                return new ServiceResponse<OrderModel> { Success = false, Message = ex.Message };
            }
        }

        public async Task<SimpleServiceResponse> UpdateOrderAsync(OrderModel order)
        {
            try
            {
                string? userId = userService.GetUserId();
                if (!string.IsNullOrWhiteSpace(userId))
                    await userData.UpdateOrderAsync(order);
                else
                    await userData.UpdateOrderAsync(order);
                return new SimpleServiceResponse { Success = true };
            }
            catch (Exception ex)
            {
                return new SimpleServiceResponse { Success = false, Message = ex.Message };
            }
        }
    }
}
