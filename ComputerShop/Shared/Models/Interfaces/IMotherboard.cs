namespace ComputerShop.Shared.Models.Interfaces
{
    public interface IMotherboard
    {
        public string Name { get; set; }
        public string Manufacturer { get; set; }
        public string Socket { get; set; }
        public int RamSlotsCount { get; set; }
        public string RamType { get; set; }
        public string Chipset { get; set; }
        public int UsbCCount { get; set; }
        public int Usb3Gen1Count { get; set; }
        public int Usb3Gen2Count { get; set; }
        public int Usb2Count { get; set; }
    }
    public interface IDesktopMotherboard : IMotherboard
    {
        public int PcieX16SlotsCount { get; set; }
        public int M2SlotsCount { get; set; }
        public int SataConnectorsCount { get; set; }
        public string Size { get; set; }
    }
}
