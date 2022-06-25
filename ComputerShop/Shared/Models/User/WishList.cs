using ComputerShop.Shared.Models.Products;

namespace ComputerShop.Shared.Models.User
{
    public class WishListModel
    {
        public WishListModel() { }
        public WishListModel(List<WishListItem> items)
        {
            List = items;
        }
        public List<WishListItem> List { get; set; } = new();
    }
    public class WishListItem
    {
        public string ProductId { get; set; }
        public DateTime Date { get; set; } = DateTime.Today;
    }
    public static class WishListExtensions
    {
        public static bool AddProduct(this WishListModel wishList, Product product)
        {
            if (!string.IsNullOrWhiteSpace(product?.Id))
            {
                wishList.List.Add(new WishListItem { ProductId = product.Id });
                return true;
            }
            return false;
        }
        public static bool AddProduct(this WishListModel wishList, string productId)
        {
            if (!string.IsNullOrWhiteSpace(productId))
            {
                wishList.List.Add(new WishListItem { ProductId = productId });
                return true;
            }
            return false;
        }
        public static bool RemoveProduct(this WishListModel wishList, string productId)
        {
            if (!string.IsNullOrWhiteSpace(productId))
            {
                WishListItem toRemove = wishList.List.FirstOrDefault(item => item.ProductId.Contains(productId));
                if(toRemove != null)
                {
                    wishList.List.Remove(toRemove);
                    return true;
                }
            }
            return false;
        }
        public static List<string> GetAllProductIds(this WishListModel wishList)
        {
            return wishList.List.Select(item => item.ProductId).ToList();
        }
    }
}
