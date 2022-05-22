using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ComputerShop.Shared.Models.User
{
    public class OrderModel
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        public List<CartItem> CartItems { get; set; } = new();
        public string State { get; set; }
        public decimal Total { get; set; }
        public DeliveryDetails DeliveryDetails { get; set; }
        public InvoiceDetails InvoiceDetails { get; set; }
    }

    public static class OrderStates
    {
        public static string Unpaid { get; } = "Nieopłacone";
        public static string Canceled { get; } = "Anulowane";
        public static string InPreparation { get; } = "W trakcie przygotowania";
        public static string InDeliver { get; } = "W trakcie dostawie";
        public static string Delivered { get; } = "Dostarczone";
    }
}