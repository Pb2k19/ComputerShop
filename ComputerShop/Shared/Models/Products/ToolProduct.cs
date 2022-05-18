using ComputerShop.Shared.Models.Products.Interfaces;

namespace ComputerShop.Shared.Models.Products
{
    public class ToolProduct : Product, IToolProduct
    {
        public ToolProduct()
        {
            Category = "Tool";
        }
        public string ToolType { get; set; }
        public int Widthmm { get; set; }
        public int Heightmm { get; set; }
        public int Lenghtmm { get; set; }
    }
}
