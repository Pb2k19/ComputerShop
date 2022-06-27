using ComputerShop.Server.Services.User;
using ComputerShop.Shared.Models;
using ComputerShop.Shared.Models.User;
using ComputerShop.Server.Services.Products;
using ComputerShop.Server.DataAccess;
using ComputerShop.Shared.Models.Products;

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

        public async Task<ServiceResponse<OrderModel>> AddOrderAsync(List<CartItem> cartItems, DeliveryDetails deliveryDetails, InvoiceDetails invoiceDetails, bool isAuthenticated)
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

            OrderModel order = new() { CartItems = cartItems, State = deliveryDetails.PaymentMethod.Equals("Przy odbiorze") ? OrderStates.InPreparation : OrderStates.Unpaid, Total = cartSum, DeliveryDetails = deliveryDetails, InvoiceDetails = invoiceDetails };
            order.SetId();
            UserModel? user;

            if(isAuthenticated)
            {
                string? userId = userService.GetUserId();
                if (!string.IsNullOrEmpty(userId))
                {
                    user = await userService.GetUserByIdAsync(userId);
                    if (user != null)
                    {
                        user.Orders.Add(order);
                        var response = await userService.UpdateUserAsync(user);
                        if (response.Success)
                            return new ServiceResponse<OrderModel> { Success = true, Data = order };
                        else
                            return new ServiceResponse<OrderModel> { Success = false, Message = response.Message };
                    }
                }
                return new ServiceResponse<OrderModel> { Success = false, Message = "Coś poszło nie tak - zamówienie nie zostało żłożone" };
            }
            
            user = new UnregisteredUser { Email = deliveryDetails.Email, Orders = new() { order } };
            var result = await userService.AddUserAsync(user);
            if(result.Success)
                return new ServiceResponse<OrderModel> { Success = true, Data = order };
            else
                return new ServiceResponse<OrderModel> { Success = false, Message = "Coś poszło nie tak - zamówienie nie zostało żłożone" };
        }

        public async Task<ServiceResponse<List<OrderModel>>> GetAllOrdersForUserAsync()
        {
            string? userId = userService.GetUserId();
            if (string.IsNullOrWhiteSpace(userId))
                return new ServiceResponse<List<OrderModel>> { Message = "Coś poszło nie tak", Success = false };
            UserModel? user = await userService.GetUserByIdAsync(userId);
            if (user == null)
                return new ServiceResponse<List<OrderModel>> { Message = "Coś poszło nie tak", Success = false };
            return new ServiceResponse<List<OrderModel>> { Data = user.Orders };
        }

        public async Task<ServiceResponse<List<OrderModel>>> GetAllOrdersAsync()
        {
            var users = await userData.GetAllUsersAsync();
            List<OrderModel> orders = new();
            users.ForEach(u => orders.AddRange(u.Orders));
            return new ServiceResponse<List<OrderModel>> { Data = orders, Success = true };
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

        public async Task<ServiceResponse<OrderModel>> GetOrderForUserByEmailAsync(string email)
        {
            try
            {
                string? userId = (await userData.GetAnyUserByEmailAsync(email))?.Id;
                OrderModel? order;
                if (!string.IsNullOrWhiteSpace(userId))
                    order = await userData.GetFirstUnpaidOrderAsync(userId);
                else
                    return new ServiceResponse<OrderModel> { Success = false, Message = "Nie znaleziono zamówienia" };

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