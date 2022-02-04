using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ComputerShop.Shared.Models
{
    public class Category
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; } = "Uncategorized";
        public string Icon { get; set; } = "grid-three-up";
        public string Url { get; set; } = "/uncategorized";
    }
}
