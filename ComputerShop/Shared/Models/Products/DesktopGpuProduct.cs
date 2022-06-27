using ComputerShop.Shared.Models.Products.Interfaces;

namespace ComputerShop.Shared.Models.Products
{
    public class DesktopGpuProduct : Product, IDesktopGpuProduct
    {
        public DesktopGpuProduct()
        {
            Category = "GPU";
        }
        public string ChipManufacturer { get; set; } = string.Empty;
        public int FrequencyMHz { get; set; }
        public int MemoryFrequencyMHz { get; set; }
        public string VramType { get; set; } = string.Empty;
        public int VramSizeGB { get; set; }
        public int Tdp { get; set; }
        public int Lenghtmm { get; set; }
        public List<StringValue> PortsList { get; set; } = new();
        public int BusWidth { get; set; }
        public List<StringValue> PowerConnectors { get; set; } = new();
    }
}
