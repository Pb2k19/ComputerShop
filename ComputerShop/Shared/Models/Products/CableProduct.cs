using ComputerShop.Shared.Models.Products.Interfaces;

namespace ComputerShop.Shared.Models.Products
{
    public class CableProduct : Product, ICableProduct
    {
        public CableProduct()
        {
            Category = new Category { Url = "cabel", Name = "Cabel", Id="49185" };
        }
        public int Lenghtmm {get; set;}
        public string CabelType { get; set; }
        public string ConnectorA { get; set; }
        public string ConnectorB { get; set; }
        public string Color { get; set; }
    }
}
