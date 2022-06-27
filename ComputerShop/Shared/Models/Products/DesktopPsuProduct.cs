using ComputerShop.Shared.Models.Products.Interfaces;

namespace ComputerShop.Shared.Models.Products
{
    public class DesktopPsuProduct : Product, IDesktopPsuProduct
    {
        public DesktopPsuProduct()
        {
            Category = "PSU";
        }
        public int Pcie8pinCount { get; set; }
        public int MolexCount { get; set; }
        public int Pcie6pinCount { get; set; }
        public bool IsModular { get; set; }
        public int FansCount { get; set; }
        public string Certificate { get; set; } = string.Empty;
        public int Power { get; set; }
        public int SataCount { get; set; }
        public List<StringValue> Protections { get; set; } = new();
    }
}
