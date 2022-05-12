﻿using ComputerShop.Shared.Models;
using ComputerShop.Shared.Models.User;

namespace ComputerShop.Server.Services.Order
{
    public interface IOrderService
    {
        Task<ServiceResponse<OrderModel>> AddOrderAsync(List<CartItem> cartItems, DeliveryDetails deliveryDetails, InvoiceDetails invoiceDetails);
    }
}
