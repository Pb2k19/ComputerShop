namespace ComputerShop.Shared.Models.User
{
    public class User
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.UtcNow;
        public List<Order> Orders { get; set; } = new();
        public List<CartItem> WishList { get; set; } = new();

    }
}
