using ComputerShop.Shared.Models.Interfaces;
using ComputerShop.Shared.Models.Products;

namespace ComputerShop.Shared.Models
{
    public class Gpu : IGpu
    {
        public string Manufacturer { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string VramType { get; set; } = string.Empty;
        public int VramSizeGB { get; set; }
        public int FrequencyMHz { get; set; }
        public int Tdp { get; set; }
        public string ChipManufacturer { get; set; } = string.Empty;
        public int MemoryFrequencyMHz { get; set; }
        public List<StringValue> PortsList { get; set; } = new();
        public int BusWidth { get; set; }
    }
    public enum GpuPorts
    {
        DVI,
        VGA,
        HDMI,
        DispalyPort,
        USB
    }
    public enum GpuManufacturers
    {
        Amd,
        Intel,
        Nvidia,
    }
    public enum GpuVramTypes
    {
        GDDR5,
        GDDR6,
        XGDDR5,
        XGDDR6,
    }
}
