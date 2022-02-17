using ComputerShop.Shared.Models.Components;
using ComputerShop.Shared.Models.Products.Interfaces;

namespace ComputerShop.Shared.Models.Products
{
    public class LaptopProduct : Product, ILaptopProduct
    {
        public LaptopProduct()
        {
            Category = new()
            {
                Id = "7", //tmp
                Icon = "fas fa-desktop",
                Name = "Laptop",
                Url = "laptop"
            };
        }
        public decimal DisplaySize { get; set; }
        public Cpu Cpu { get; set; }
        public Gpu Gpu { get; set; }
        public Ram Ram { get; set; }
        public List<Drive> Drives { get; set; }
        public Psu Psu { get; set; }
        public int BatteryCapacitymAh { get; set; }
        public Motherboard Motherboard { get; set; }
        public int Widthmm { get; set; }
        public int Heightmm { get; set; }
        public int Lenghtmm { get; set; }
        public int BatteryLifeIdleMin { get; set; }
        public int BatteryLifeUnderLoadMin { get; set; }
    }
}
