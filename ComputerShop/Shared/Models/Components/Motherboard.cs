using ComputerShop.Shared.Models.Interfaces;

namespace ComputerShop.Shared.Models.Components
{
    public class Motherboard : IMotherboard
    {
        public string Name { get; set; }
        public string Socket { get; set; }
        public string RamType { get; set; }
        public string Chipset { get; set; }
        public int UsbCCount { get; set; }
        public int Usb3Gen1Count { get; set; }
        public int Usb3Gen2Count { get; set; }
    }
}
