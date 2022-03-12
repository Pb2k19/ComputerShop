using ComputerShop.Shared.Models.Products.Interfaces;

namespace ComputerShop.Shared.Models.Products
{
    public class ToolProduct : Product, IToolProduct
    {
        public ToolProduct()
        {
            Category = new Category { Name = "Tool", Url = "tool", Id="2535423" };
        }
        public string ToolType { get; set; }
        public int Widthmm { get; set; }
        public int Heightmm { get; set; }
        public int Lenghtmm { get; set; }
    }
}
