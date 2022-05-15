namespace ComputerShop.Shared.Models.User
{
    public class UserModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.UtcNow;
        public List<OrderModel> Orders { get; set; } = new();
        public WishListModel WishList { get; set; } = new();
        public DeliveryDetails DeliveryDetails { get; set; } = new();
        public InvoiceDetails InvoiceDetails { get; set; } = new();
    }
}