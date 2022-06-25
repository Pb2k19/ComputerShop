using ComputerShop.Shared.Models;
using ComputerShop.Shared.Models.Products;

namespace ComputerShop.Server.Helpers
{
    public class ProductHelper
    {
        public IEnumerable<Product> SortProducts(ref IEnumerable<Product> products, int mode)
        {
            if (mode == SortOptions.OrderByName.Option)
                return products.OrderBy(x => $"{x.Manufacturer} {x.Name}");
            else if (mode == SortOptions.OrderByNameInverted.Option)
                return products.OrderByDescending(x => $"{x.Manufacturer} {x.Name}");
            else if (mode == SortOptions.OrderByPriceAscending.Option)
                return products.OrderByDescending(x => x.Price);
            else if (mode == SortOptions.OrderByPriceDescending.Option)
                return products.OrderBy(x => x.Price);
            return products;
        }
        public IEnumerable<Product> FilterOnlyAvaliableProducts(IEnumerable<Product> products)
        {
            return products.Where(p => p.IsAvaliable);
        }
        public IEnumerable<Product> FilterOnlyDiscountedProducts(IEnumerable<Product> products)
        {
            return products.Where(p => p.PriceBeforeDiscount > p.Price);
        }
        public IEnumerable<Product> FilterManufacturers(IEnumerable<Product> products, IEnumerable<string> manufacturers)
        {
            return products.Where(p => manufacturers.Contains(p.Manufacturer));
        }
        public IEnumerable<Product> PriceFilter(IEnumerable<Product> products, int priceMax, int priceMin = 0)
        {
            return products.Where(p => p.Price >= priceMin && p.Price <= priceMax);
        }
        public IEnumerable<Product> SortFilterProducts(IEnumerable<Product> products, ProductSortFilterOptions sortFilterOptions)
        {
            if (sortFilterOptions.ShowAvaliableOnly)
                products = FilterOnlyAvaliableProducts(products);
            if(sortFilterOptions.ShowDiscountOnly)
                products = FilterOnlyDiscountedProducts(products);
            if (sortFilterOptions.Manufacturers != null && sortFilterOptions.Manufacturers.Count > 0)
                products = FilterManufacturers(products, sortFilterOptions.Manufacturers);
            products = PriceFilter(products, sortFilterOptions.PriceMax, sortFilterOptions.PriceMin);
            return SortProducts(ref products, sortFilterOptions.SortOption); ;
        }
    }
}