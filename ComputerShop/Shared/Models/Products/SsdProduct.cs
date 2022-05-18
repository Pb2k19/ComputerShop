using ComputerShop.Shared.Models.Products.Interfaces;

namespace ComputerShop.Shared.Models.Products
{
    public class SsdProduct : Product, ISsdProduct
    {
        public SsdProduct()
        {
            Category = "SSD";
        }
        public int Tbw { get; set; }
        public string Interface { get; set; }
        public int ReadSpeedMBs { get; set; }
        public int WriteSpeedMBs { get; set; }
        public string Type { get; set; }
        public int CapacityGB { get; set; }
    }
}
