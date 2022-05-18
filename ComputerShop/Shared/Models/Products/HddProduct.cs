using ComputerShop.Shared.Models.Products.Interfaces;

namespace ComputerShop.Shared.Models.Products
{
    public class HddProduct : Product, IHddProduct
    {
        public HddProduct()
        {
            Category = "HDD";
        }
        public int Rpm {get; set;}
        public int ReadSpeedMBs { get; set; }
        public int WriteSpeedMBs { get; set; }
        public string Type { get; set; }
        public int CapacityGB { get; set; }
        public string Interface { get; set; }
    }
}
