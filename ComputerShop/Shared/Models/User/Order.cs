namespace ComputerShop.Shared.Models.User
{
    public class Order
    {
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        public List<CartItem> CartItems { get; set; } = new();
    }
}
