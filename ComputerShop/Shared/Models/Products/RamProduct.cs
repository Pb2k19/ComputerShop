using ComputerShop.Shared.Models.Products.Interfaces;

namespace ComputerShop.Shared.Models.Products
{
    public class RamProduct : Product, IRamProduct
    {
        public RamProduct()
        {
            Category = "RAM";
        }
        public int ModulesNumber { get; set; }
        public int FrequencyMHz { get; set; }
        public int LatencyCL { get; set; }
        public string RamTechnology { get; set; } = string.Empty;
    }
}
