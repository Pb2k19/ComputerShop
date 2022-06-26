using ComputerShop.Shared.Models.Interfaces;
using ComputerShop.Shared.Models.Products;

namespace ComputerShop.Shared.Models
{
    public class Gpu : IGpu
    {
        public string Manufacturer { get; set; }
        public string Name { get; set; }
        public string VramType { get; set; }
        public int VramSizeGB { get; set; }
        public int FrequencyMHz { get; set; }
        public int Tdp { get; set; }
        public string ChipManufacturer { get; set; }
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
