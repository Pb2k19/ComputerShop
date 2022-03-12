using ComputerShop.Shared.Models.Interfaces;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ComputerShop.Shared.Models
{
    public class Product : IProduct
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Manufacturer { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal PriceBeforeDiscount { get; set; }
        public bool IsPublic { get; set; }
        public bool IsRemoved { get; set; }
        public DateTime CareationDate { get; set; }
        public DateTime LastUpdateDate { get; set; }
        public Category Category { get; set; }
        public List<Image> Images { get; set; } = new List<Image> { new Image()};
        public int WarantyMonths { get; set; } = 12;
        public List<Prop> ExtraInfo { get; set; }
    }
}
