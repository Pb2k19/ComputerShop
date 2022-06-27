using ComputerShop.Shared.Models.Interfaces;

namespace ComputerShop.Shared.Models.ComputerComponents
{
    public class Motherboard : IMotherboard
    {
        public string Name { get; set; } = string.Empty;
        public string Socket { get; set; } = string.Empty;
        public int RamSlotsCount { get; set; }
        public string RamType { get; set; } = string.Empty;
        public string Chipset { get; set; } = string.Empty;
        public int UsbCCount { get; set; }
        public int Usb3Gen1Count { get; set; }
        public int Usb3Gen2Count { get; set; }
        public string Manufacturer { get; set; } = string.Empty;
        public int Usb2Count { get; set; }
    }
}
