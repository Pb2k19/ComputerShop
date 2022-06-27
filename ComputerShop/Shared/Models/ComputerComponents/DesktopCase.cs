using ComputerShop.Shared.Models.Interfaces;
using ComputerShop.Shared.Models.Products;

namespace ComputerShop.Shared.Models.ComputerComponents
{
    public class DesktopCase : IComputerCase
    {
        public string Name { get; set; } = string.Empty;
        public string Manufacturer { get; set; } = string.Empty;
        public int Lenghtmm { get; set; }
        public int Heightmm { get; set; }
        public int Widthmm { get; set; }
        public List<StringValue> SupportedMoboSizes { get; set; } = new();
        public int UsbPorts { get; set; }
        public int MaxFanCount { get; set; }
        public int MaxCoolerHeightmm { get; set; }
        public int MaxRadiatorSizemm { get; set; }
        public int MaxGpuLenghtmm { get; set; }
        public string Color { get; set; } = string.Empty;
    }
}
