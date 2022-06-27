using ComputerShop.Shared.Models.Products.Interfaces;

namespace ComputerShop.Shared.Models.Products
{
    public class HeadphonesProduct : Product, IHeadphonesProduct
    {
        public HeadphonesProduct()
        {
            Category = "Headphones";
        }
        public bool IsWireless {get; set;}
        public string HeadphonesType { get; set; } = string.Empty;
        public int ImpedanceOhm { get; set; }
        public int Weightg { get; set; }
        public int MinFrequencyResponseHz { get; set; }
        public int MaxFrequencyResponsekHz { get; set; }
        public string Interface { get; set; } = string.Empty;
        public int Widthmm { get; set; }
        public int Heightmm { get; set; }
        public int Lenghtmm { get; set; }
    }
}
