using ComputerShop.Shared.Models.ComputerComponents;
using ComputerShop.Shared.Models.Interfaces;
using ComputerShop.Shared.Models.Products.Interfaces;

namespace ComputerShop.Shared.Models.Products
{
    public class LaptopProduct : Product, ILaptopProduct
    {
        public LaptopProduct()
        {
            Category = "Laptop";
        }
        public decimal DisplaySize { get; set; }
        public Cpu Cpu { get; set; } = new();
        public Gpu Gpu { get; set; } = new();
        public Ram Ram { get; set; } = new();
        public List<Drive> Drives { get; set; } = new();
        public Psu Psu { get; set; } = new();
        public Motherboard Motherboard { get; set; } = new();
        public int BatteryCapacitymAh { get; set; }
        public int BatteryLifeIdleMin { get; set; }
        public int BatteryLifeUnderLoadMin { get; set; }
        public int Widthmm { get; set; }
        public int Heightmm { get; set; }
        public int Lenghtmm { get; set; }
    }
}
