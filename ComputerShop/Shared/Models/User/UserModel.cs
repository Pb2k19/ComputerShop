using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ComputerShop.Shared.Models.User
{
    [BsonKnownTypes(typeof(RegisteredUser), typeof(UnregisteredUser))]
    public abstract class UserModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Email { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.UtcNow;
        public List<OrderModel> Orders { get; set; } = new();
    }

    public class RegisteredUser : UserModel
    {
        public string Password { get; set; }
        public string Role { get; set; } = "Customer";
        public WishListModel WishList { get; set; } = new();
        public DeliveryDetails DeliveryDetails { get; set; } = new();
        public InvoiceDetails InvoiceDetails { get; set; } = new();        
    }

    public class UnregisteredUser : UserModel
    {

    }
}