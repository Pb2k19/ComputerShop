using ComputerShop.Shared.Models.Interfaces;
using ComputerShop.Shared.Models.Products.Interfaces;

namespace ComputerShop.Shared.Models.Products
{
    public class MotherboardProduct : Product, IDesktopMotherboardProduct
    {
        public MotherboardProduct()
        {
            Category = "Motherboard";
        }
        public int PcieX16SlotsCount { get; set; }
        public int RamSlotsCount { get; set; }
        public int M2SlotsCount { get; set; }
        public int SataConnectorsCount { get; set; }
        public string Socket { get; set; } = string.Empty;
        public string RamType { get; set; } = string.Empty;
        public string Chipset { get; set; } = string.Empty;
        public int UsbCCount { get; set; }
        public int Usb3Gen1Count { get; set; }
        public int Usb3Gen2Count { get; set; }        
        public int Usb2Count { get; set; }
        public string Size { get; set; } = string.Empty;
    }

    public static class MotherboardSizes
    {
        public static string ExtendedAtx { get; } = "Extended ATX";
        public static string StandardAtx { get; } = "Standard ATX";
        public static string MiniATX { get; } = "Mini ATX";
        public static string MicroATX { get; } = "microATX";
        public static string FlexATX { get; } = "FlexATX";
    }
}
