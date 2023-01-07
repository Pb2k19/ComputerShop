using ComputerShop.Shared.Models.Interfaces;

namespace ComputerShop.Shared.Models.Products
{
    public class Product : IProduct
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Manufacturer { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public decimal PriceBeforeDiscount { get; set; }
        public bool IsPublic { get; set; }
        public bool IsHiglighted { get; set; } = false;
        public bool IsRemoved { get; set; }
        public bool IsAvaliable { get; set; } = true;
        public DateTime CareationDate { get; set; } = DateTime.UtcNow;
        public DateTime LastUpdateDate { get; set; } = DateTime.UtcNow;
        public string Category { get; set; } = string.Empty;
        public List<Image> Images { get; set; } = new();
        public int WarantyMonths { get; set; } = 12;
        public List<KeyValue> ExtraInfo { get; set; } = new();
        public List<Comment> Comments { get; set; } = new();
    }
}