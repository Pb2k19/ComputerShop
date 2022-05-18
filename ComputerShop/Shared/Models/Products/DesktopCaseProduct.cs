using ComputerShop.Shared.Models.Products.Interfaces;

namespace ComputerShop.Shared.Models.Products
{
    public class DesktopCaseProduct : Product, IComputerCaseProduct
    {
        public DesktopCaseProduct()
        {
            Category = "Case";
        }
        public int Lenghtmm {get; set;}
        public int Heightmm { get; set; }
        public int Widthmm { get; set; }
        public List<string> SupportedMoboSizes { get; set; }
        public int UsbPorts { get; set; }
        public int MaxFanCount { get; set; }
        public int MaxCoolerHeightmm { get; set; }
        public int MaxRadiatorSizemm { get; set; }
        public int MaxGpuLenghtmm { get; set; }
        public string Color { get; set; }
    }
}
