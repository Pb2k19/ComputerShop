using ComputerShop.Shared.Models.Products;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ComputerShop.Shared.Models.Interfaces
{
    public interface IProduct
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Manufacturer { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
        public decimal PriceBeforeDiscount { get; set; }
        public bool IsPublic { get; set; }
        public bool IsRemoved { get; set; }
        public bool IsAvaliable { get; set; }
        public DateTime CareationDate { get; set; }
        public DateTime LastUpdateDate { get; set; }
        public int WarantyMonths { get; set; }
        public List<Image> Images { get; set; }
        public List<KeyValue> ExtraInfo { get; set; }
        public List<Comment> Comments {get; set;}
    }
}
