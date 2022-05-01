namespace ComputerShop.Shared.Models.User
{
    public class WishLisModel
    {
        public List<WishListItem> List { get; set; } = new();
    }
    public class WishListItem
    {
        public string ProductId { get; set; }
        public DateTime Date { get; set; } = DateTime.Today;
    }
    public static class WishListExtensions
    {
        public static void AddProduct(this WishLisModel wishList, Product product)
        {
            if (!string.IsNullOrWhiteSpace(product?.Id))
            {
                wishList.List.Add(new WishListItem { ProductId = product.Id });
            }
        }
        public static void AddProduct(this WishLisModel wishList, string productId)
        {
            if (!string.IsNullOrWhiteSpace(productId))
            {
                wishList.List.Add(new WishListItem { ProductId = productId });
            }
        }
        public static void RemoveProduct(this WishLisModel wishList, string productId)
        {
            if (!string.IsNullOrWhiteSpace(productId))
            {
                WishListItem toRemove = wishList.List.FirstOrDefault(item => item.ProductId.Contains(productId));
                if(toRemove != null)
                    wishList.List.Remove(toRemove);
            }
        }
    }
}
