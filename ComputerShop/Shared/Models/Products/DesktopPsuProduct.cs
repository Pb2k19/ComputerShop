using ComputerShop.Shared.Models.Products.Interfaces;

namespace ComputerShop.Shared.Models.Products
{
    public class DesktopPsuProduct : Product, IDesktopPsuProduct
    {
        public int Pcie8pinCount { get; set; }
        public int MolexCount { get; set; }
        public int Pcie6pinCount { get; set; }
        public bool IsModular { get; set; }
        public int FansCount { get; set; }
        public string Certificate { get; set; }
        public int Power { get; set; }
    }
}
