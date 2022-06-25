using ComputerShop.Shared.Models.Interfaces;

namespace ComputerShop.Shared.Models.ComputerComponents
{
    public class Motherboard : IMotherboard
    {
        public string Name { get; set; }
        public string Socket { get; set; }
        public int RamSlotsCount { get; set; }
        public string RamType { get; set; }
        public string Chipset { get; set; }
        public int UsbCCount { get; set; }
        public int Usb3Gen1Count { get; set; }
        public int Usb3Gen2Count { get; set; }
        public string Manufacturer { get; set; }
        public int Usb2Count { get; set; }
    }
}
