namespace ComputerShop.Shared.Models.Products
{
    public class ProductsResponse
    {
        public List<Product> Products { get; set; } = new();
        public HashSet<string> Manufacturers { get; set; } = new();
        public int CurrentPage { get; set; } = 1;
        public int PagesCount { get; set; } = 1;
    }
}
