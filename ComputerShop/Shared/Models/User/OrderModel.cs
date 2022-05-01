﻿namespace ComputerShop.Shared.Models.User
{
    public class OrderModel
    {
        public string Id { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        public List<CartItem> CartItems { get; set; } = new();
        public string State { get; set; }
        public decimal Total { get; set; }
        public DeliveryDetails DeliveryDetails { get; set; }
    }
}