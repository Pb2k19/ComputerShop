namespace ComputerShop.Shared.Models
{
    public class ProductsResponse
    {
        public List<Product> Products { get; set; }
        public int CurrentPage { get; set; }
        public int PagesCount { get; set; }        
    }
}
