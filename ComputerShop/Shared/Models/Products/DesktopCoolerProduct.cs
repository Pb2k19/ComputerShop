using ComputerShop.Shared.Models.Products.Interfaces;

namespace ComputerShop.Shared.Models.Products
{
    public class DesktopCoolerProduct : Product, IDesktopCoolerProduct
    {
        public DesktopCoolerProduct()
        {
            Category = "Cooler";
        }
        public int FansCount { get; set; }
        public int MaxTdp { get; set; }
        public List<StringValue> CompatibleSockets { get; set; } = new();
        public string CoolerType { get; set; } = string.Empty;
        public int Sizemm { get; set; }
    }
}
