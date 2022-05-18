using ComputerShop.Shared.Models.Products.Interfaces;

namespace ComputerShop.Shared.Models.Products
{
    public class CableProduct : Product, ICableProduct
    {
        public CableProduct()
        {
            Category = "Cabel";
        }
        public int Lenghtmm {get; set;}
        public string CabelType { get; set; }
        public string ConnectorA { get; set; }
        public string ConnectorB { get; set; }
        public string Color { get; set; }
    }
}
