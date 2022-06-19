using ComputerShop.Shared.Models.Components;
using ComputerShop.Shared.Models.Interfaces;
using ComputerShop.Shared.Models.Products;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ComputerShop.Shared.Models
{
    [BsonKnownTypes(typeof(CableProduct), typeof(ComputerMouseProduct), typeof(CpuProduct), 
        typeof(DesktopCaseProduct), typeof(DesktopCoolerProduct), typeof(DesktopGpuProduct), 
        typeof(DesktopPcProduct), typeof(DesktopPcProduct), typeof(DesktopPsuProduct), 
        typeof(HddProduct), typeof(HeadphonesProduct), typeof(KeyboardProduct), typeof(LaptopProduct), 
        typeof(MonitorProduct), typeof(MotherboardProduct), typeof(PrinterProduct), typeof(RamProduct), 
        typeof(SsdProduct), typeof(ToolProduct))]
    public class Product : IProduct
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Manufacturer { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal PriceBeforeDiscount { get; set; }
        public bool IsPublic { get; set; }
        public bool IsHiglighted { get; set; } = false;
        public bool IsRemoved { get; set; }
        public bool IsAvaliable { get; set; }
        public DateTime CareationDate { get; set; } = DateTime.UtcNow;
        public DateTime LastUpdateDate { get; set; } = DateTime.UtcNow;
        public string Category { get; set; }
        public List<Image> Images { get; set; } = new() { new Image() };
        public int WarantyMonths { get; set; } = 12;
        public List<KeyValue> ExtraInfo { get; set; }
        public List<Comment> Comments { get; set; } = new();
    }
}
