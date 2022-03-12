using ComputerShop.Shared.Models.Products.Interfaces;

namespace ComputerShop.Shared.Models.Products
{
    public class HeadphonesProduct : Product, IHeadphonesProduct
    {
        public HeadphonesProduct()
        {
            Category = new Category { Name = "Headphones", Url = "headphones", Id="415" };
        }
        public bool IsWireless {get; set;}
        public string HeadphonesType { get; set; }
        public int ImpedanceOhm { get; set; }
        public int Weightg { get; set; }
        public int MinFrequencyResponseHz { get; set; }
        public int MaxFrequencyResponsekHz { get; set; }
        public string Interface { get; set; }
        public int Widthmm { get; set; }
        public int Heightmm { get; set; }
        public int Lenghtmm { get; set; }
    }
}
