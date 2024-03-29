﻿using ComputerShop.Shared.Models;
using ComputerShop.Shared.Models.Interfaces;
using ComputerShop.Shared.Models.Products;
using ComputerShop.Shared.Models.Products.Interfaces;
using Newtonsoft.Json;

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
        public virtual Product? DeserializePorduct(string category, string json)
        {
            return category.ToLower() switch
            {
                "pc" => JsonConvert.DeserializeObject<DesktopPcProduct>(json),
                "psu" => JsonConvert.DeserializeObject<DesktopPsuProduct>(json),
                "gpu" => JsonConvert.DeserializeObject<DesktopGpuProduct>(json),
                "laptop" => JsonConvert.DeserializeObject<LaptopProduct>(json),
                "cpu" => JsonConvert.DeserializeObject<CpuProduct>(json),
                "motherboard" => JsonConvert.DeserializeObject<MotherboardProduct>(json),
                "ram" => JsonConvert.DeserializeObject<RamProduct>(json),
                "hdd" => JsonConvert.DeserializeObject<HddProduct>(json),
                "ssd" => JsonConvert.DeserializeObject<SsdProduct>(json),
                "case" => JsonConvert.DeserializeObject<DesktopCaseProduct>(json),
                "keyboard" => JsonConvert.DeserializeObject<KeyboardProduct>(json),
                "headphones" => JsonConvert.DeserializeObject<HeadphonesProduct>(json),
                "monitor" => JsonConvert.DeserializeObject<MonitorProduct>(json),
                "cabel" => JsonConvert.DeserializeObject<CableProduct>(json),
                "cooler" => JsonConvert.DeserializeObject<DesktopCoolerProduct>(json),
                "mouse" => JsonConvert.DeserializeObject<DesktopCoolerProduct>(json),
                _ => null,
            };
        }
    }
}