using ComputerShop.Shared.Models.ComputerComponents;
using ComputerShop.Shared.Models.Products.Interfaces;
using MongoDB.Bson.Serialization.Attributes;

namespace ComputerShop.Shared.Models.Products
{
    public class DesktopPcProduct : Product, IDesktopPcProduct
    {
        public DesktopPcProduct()
        {
            Category = "PC";
        }
        public Cpu Cpu { get; set; } = new();
        public Ram Ram { get; set; } = new();
        public Gpu Gpu { get; set; } = new();
        public Psu Psu { get; set; } = new();
        public Motherboard Motherboard { get; set; } = new();
        public DesktopCase DesktopCase { get; set; } = new();
        public Cooler Cooler { get; set; } = new();
        public List<Drive> Drives { get; set; } = new();
        public int PowerConsumption { get; set; }
    }
}
