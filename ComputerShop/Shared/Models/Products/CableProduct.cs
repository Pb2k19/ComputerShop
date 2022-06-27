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
        public string CabelType { get; set; } = string.Empty;
        public string ConnectorA { get; set; } = string.Empty;
        public string ConnectorB { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
    }
}
