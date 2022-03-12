using ComputerShop.Shared.Models.Products.Interfaces;

namespace ComputerShop.Shared.Models.Products
{
    public class DesktopCoolerProduct : Product, IDesktopCoolerProduct
    {
        public DesktopCoolerProduct()
        {
            Category = new Category { Id = "65461", Name = "Cooler", Url = "cooler" };
        }
        public int FansCount { get; set; }
        public int MaxTdp { get; set; }
        public List<string> CompatibleSockets { get; set; }
        public string CoolerType { get; set; }
        public int Sizemm { get; set; }
    }
}
