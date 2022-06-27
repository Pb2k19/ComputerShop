using ComputerShop.Shared.Models.Interfaces;
using MongoDB.Bson.Serialization.Attributes;

namespace ComputerShop.Shared.Models.ComputerComponents
{
    [BsonKnownTypes(typeof(Ssd), typeof(Hdd))]
    public class Drive : IDrive
    {
        public string Manufacturer { get; set; } = string.Empty;
        public int ReadSpeedMBs { get; set; }
        public int WriteSpeedMBs { get; set; }
        public string Type { get; set; } = string.Empty;
        public int CapacityGB { get; set; }
        public string Interface { get; set; } = string.Empty;
    }
}
