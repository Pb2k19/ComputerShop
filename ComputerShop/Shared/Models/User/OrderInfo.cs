namespace ComputerShop.Shared.Models.User
{
    public class OrderInfo
    {
        public List<CartItem> CartItems { get; set; }
        public DeliveryDetails DeliveryDetails { get; set; }
    }
}
