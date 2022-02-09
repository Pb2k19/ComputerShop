using ComputerShop.Shared.Models.Components;
using ComputerShop.Shared.Models.Products.Interfaces;

namespace ComputerShop.Shared.Models.Products
{
    public class LaptopProduct : Product, ILaptopProduct
    {
        public decimal DisplaySize { get; set; }
        public Cpu Cpu { get; set; }
        public Gpu Gpu { get; set; }
        public Ram Ram { get; set; }
        public List<Drive> Drives { get; set; }
        public Psu Psu { get; set; }
        public int BatteryCapacitymAh { get; set; }
        public Motherboard Motherboard { get; set; }
    }
}
