using ComputerShop.Shared.Models.Products.Interfaces;

namespace ComputerShop.Shared.Models.Products
{
    public class KeyboardProduct : Product, IKeyboardProduct
    {
        public KeyboardProduct()
        {
            Category = "Keyboard";
        }
        public string KeyboardType {get; set;} = string.Empty;
        public bool IsWireless {get;set;}
        public string Size { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public int Weightg { get; set; }
        public int Widthmm { get; set; }
        public int Heightmm { get; set; }
        public int Lenghtmm { get; set; }
        public string Interface { get; set; } = string.Empty;
    }
}
