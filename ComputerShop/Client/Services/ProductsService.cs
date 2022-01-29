using ComputerShop.Shared.Models;

namespace ComputerShop.Client.Services
{
    public class ProductsService : IProductsService
    {
        public List<Product> Products { get; set; } = new List<Product>();

        public void Load()
        {
            Products = new List<Product>
            {
                new Product
                {
                    Id = 1,
                    Name = "MSI SUPER B450 GAMING PLUS MAX",
                    CareationDate = DateTime.Today,
                    Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aliquam accumsan ipsum vitae lacus dignissim, at scelerisque metus luctus. Nullam magna diam, facilisis in commodo tristique, blandit quis elit. Nunc in lacinia ipsum. Nulla tincidunt a nunc ac eleifend. Maecenas fermentum sed eros eget scelerisque. Nunc a eros id elit malesuada convallis. Nam consectetur turpis et ipsum dignissim luctus. In nulla velit, fermentum ut fringilla imperdiet, tristique ut massa. Vivamus volutpat, enim at interdum suscipit, ipsum urna condimentum neque, quis luctus mi quam id sapien. Vivamus turpis enim, fermentum ac tortor in, egestas cursus enim. Sed mattis ultrices tristique. Maecenas feugiat molestie mi at sodales. Nunc in erat ex. Sed non molestie diam. Donec porttitor et orci eget dapibus. Vivamus ultrices dictum lectus ac sollicitudin.",
                    Images = new List<Image>{new Image() { Id = 1, Name = "b450", Location = "https://asset.msi.com/resize/image/global/product/product_1_20190716132640_5d2d60107d8bc.png62405b38c58fe0f07fcef2367d8a9ba1/1024.png" },
                                             new Image() { Id = 3, Name = "b450", Location = "https://asset.msi.com/resize/image/global/product/five_pictures6_3342_20150129174155.png62405b38c58fe0f07fcef2367d8a9ba1/1024.png" },
                                             new Image() { Id = 1, Name = "b450", Location = "https://upload.wikimedia.org/wikipedia/commons/thumb/3/3e/Alexander_Albon_Red_Bull_RB16.jpg/1920px-Alexander_Albon_Red_Bull_RB16.jpg" }},
                    LastUpdateDate = DateTime.Now,
                    CategoryId = 1,
                    Price = 25.99m,
                    PriceBeforeDiscount = 5000.99m,
                    IsPublic = true,
                    IsRemoved = false
                },
                new Product
                {
                    Id = 2,
                    Name = "GTX 970 100ME",
                    CareationDate = DateTime.Today,
                    Description = "Ładna karta",
                    Images = new List<Image>{new Image() { Id = 2, Name = "970_100me", Location = "https://asset.msi.com/resize/image/global/product/five_pictures6_3342_20150129174155.png62405b38c58fe0f07fcef2367d8a9ba1/1024.png" } },
                    LastUpdateDate = DateTime.Now,
                    CategoryId = 2,
                    Price = 50000.99m,
                    PriceBeforeDiscount = 600006.99m,
                    IsPublic = true,
                    IsRemoved = false
                },
                new Product
                {
                    Id = 1,
                    Name = "B450 GAMING PLUS MAX",
                    CareationDate = DateTime.Today,
                    Description = "Ładna płyta",
                    Images = new List<Image>{new Image() { Id = 1, Name = "b450", Location = "https://asset.msi.com/resize/image/global/product/product_1_20190716132640_5d2d60107d8bc.png62405b38c58fe0f07fcef2367d8a9ba1/1024.png" } },
                    LastUpdateDate = DateTime.Now,
                    CategoryId = 1,
                    Price = 25.99m,
                    PriceBeforeDiscount = 26.99m,
                    IsPublic = true,
                    IsRemoved = false
                },
                new Product
                {
                    Id = 2,
                    Name = "GTX 970 100ME",
                    CareationDate = DateTime.Today,
                    Description = "Ładna karta",
                    Images = new List<Image>{new Image() { Id = 2, Name = "970_100me", Location = "https://asset.msi.com/resize/image/global/product/five_pictures6_3342_20150129174155.png62405b38c58fe0f07fcef2367d8a9ba1/1024.png" } },
                    LastUpdateDate = DateTime.Now,
                    CategoryId = 2,
                    Price = 50000.99m,
                    PriceBeforeDiscount = 600006.99m,
                    IsPublic = true,
                    IsRemoved = false
                },
                new Product
                {
                    Id = 1,
                    Name = "B450 GAMING PLUS MAXXXX",
                    CareationDate = DateTime.Today,
                    Description = "Ładna płyta",
                    Images = new List<Image>{new Image() { Id = 1, Name = "b450", Location = "https://asset.msi.com/resize/image/global/product/product_1_20190716132640_5d2d60107d8bc.png62405b38c58fe0f07fcef2367d8a9ba1/1024.png" } },
                    LastUpdateDate = DateTime.Now,
                    CategoryId = 3,
                    Price = 25.99m,
                    PriceBeforeDiscount = 26.99m,
                    IsPublic = true,
                    IsRemoved = false
                },
                new Product
                {
                    Id = 2,
                    Name = "GTX 970 100ME",
                    CareationDate = DateTime.Today,
                    Description = "Ładna karta",
                    Images = new List<Image>{new Image() { Id = 2, Name = "970_100me", Location = "https://asset.msi.com/resize/image/global/product/five_pictures6_3342_20150129174155.png62405b38c58fe0f07fcef2367d8a9ba1/1024.png" } },
                    LastUpdateDate = DateTime.Now,
                    CategoryId = 1,
                    Price = 50000.99m,
                    PriceBeforeDiscount = 600006.99m,
                    IsPublic = true,
                    IsRemoved = false
                },
                new Product
                {
                    Id = 1,
                    Name = "B450 GAMING PLUS MAX",
                    CareationDate = DateTime.Today,
                    Description = "Ładna płyta",
                    Images = new List<Image>{new Image() { Id = 1, Name = "b450", Location = "https://asset.msi.com/resize/image/global/product/product_1_20190716132640_5d2d60107d8bc.png62405b38c58fe0f07fcef2367d8a9ba1/1024.png" } },
                    LastUpdateDate = DateTime.Now,
                    CategoryId = 1,
                    Price = 25.99m,
                    PriceBeforeDiscount = 26.99m,
                    IsPublic = true,
                    IsRemoved = false
                },
                new Product
                {
                    Id = 2,
                    Name = "GTX 970 100ME",
                    CareationDate = DateTime.Today,
                    Description = "Ładna karta",
                    Images = new List<Image>{new Image() { Id = 2, Name = "970_100me", Location = "https://asset.msi.com/resize/image/global/product/five_pictures6_3342_20150129174155.png62405b38c58fe0f07fcef2367d8a9ba1/1024.png" } },
                    LastUpdateDate = DateTime.Now,
                    CategoryId = 1,
                    Price = 50000.99m,
                    PriceBeforeDiscount = 600006.99m,
                    IsPublic = true,
                    IsRemoved = false
                },
                new Product
                {
                    Id = 1,
                    Name = "B450 GAMING PLUS MAX",
                    CareationDate = DateTime.Today,
                    Description = "Ładna płyta",
                    Images = new List<Image>{new Image() { Id = 1, Name = "b450", Location = "https://asset.msi.com/resize/image/global/product/product_1_20190716132640_5d2d60107d8bc.png62405b38c58fe0f07fcef2367d8a9ba1/1024.png" } },
                    LastUpdateDate = DateTime.Now,
                    CategoryId = 1,
                    Price = 25.99m,
                    PriceBeforeDiscount = 26.99m,
                    IsPublic = true,
                    IsRemoved = false
                },
                new Product
                {
                    Id = 2,
                    Name = "GTX 970 100ME",
                    CareationDate = DateTime.Today,
                    Description = "Ładna karta",
                    Images = new List<Image>{new Image() { Id = 2, Name = "970_100me", Location = "https://asset.msi.com/resize/image/global/product/five_pictures6_3342_20150129174155.png62405b38c58fe0f07fcef2367d8a9ba1/1024.png" } },
                    LastUpdateDate = DateTime.Now,
                    CategoryId = 1,
                    Price = 50000.99m,
                    PriceBeforeDiscount = 600006.99m,
                    IsPublic = true,
                    IsRemoved = false
                },
                new Product
                {
                    Id = 1,
                    Name = "B450 GAMING PLUS MAX",
                    CareationDate = DateTime.Today,
                    Description = "Ładna płyta",
                    Images = new List<Image>{new Image() { Id = 1, Name = "b450", Location = "https://asset.msi.com/resize/image/global/product/product_1_20190716132640_5d2d60107d8bc.png62405b38c58fe0f07fcef2367d8a9ba1/1024.png" } },
                    LastUpdateDate = DateTime.Now,
                    CategoryId = 1,
                    Price = 25.99m,
                    PriceBeforeDiscount = 26.99m,
                    IsPublic = true,
                    IsRemoved = false
                },
                new Product
                {
                    Id = 2,
                    Name = "GTX 970 100ME",
                    CareationDate = DateTime.Today,
                    Description = "Ładna karta",
                    Images = new List<Image>{new Image() { Id = 2, Name = "970_100me", Location = "https://asset.msi.com/resize/image/global/product/five_pictures6_3342_20150129174155.png62405b38c58fe0f07fcef2367d8a9ba1/1024.png" } },
                    LastUpdateDate = DateTime.Now,
                    CategoryId = 1,
                    Price = 50000.99m,
                    PriceBeforeDiscount = 600006.99m,
                    IsPublic = true,
                    IsRemoved = false
                },
                new Product
                {
                    Id = 1,
                    Name = "B450 GAMING PLUS MAX",
                    CareationDate = DateTime.Today,
                    Description = "Ładna płyta",
                    Images = new List<Image>{new Image() { Id = 1, Name = "b450", Location = "https://asset.msi.com/resize/image/global/product/product_1_20190716132640_5d2d60107d8bc.png62405b38c58fe0f07fcef2367d8a9ba1/1024.png" } },
                    LastUpdateDate = DateTime.Now,
                    CategoryId = 1,
                    Price = 25.99m,
                    PriceBeforeDiscount = 26.99m,
                    IsPublic = true,
                    IsRemoved = false
                },
                new Product
                {
                    Id = 2,
                    Name = "GTX 970 100ME",
                    CareationDate = DateTime.Today,
                    Description = "Ładna karta",
                    Images = new List<Image>{new Image() { Id = 2, Name = "970_100me", Location = "https://asset.msi.com/resize/image/global/product/five_pictures6_3342_20150129174155.png62405b38c58fe0f07fcef2367d8a9ba1/1024.png" } },
                    LastUpdateDate = DateTime.Now,
                    CategoryId = 1,
                    Price = 50000.99m,
                    PriceBeforeDiscount = 600006.99m,
                    IsPublic = true,
                    IsRemoved = false
                },
                new Product
                {
                    Id = 1,
                    Name = "B450 GAMING PLUS MAX",
                    CareationDate = DateTime.Today,
                    Description = "Ładna płyta",
                    Images = new List<Image>{new Image() { Id = 1, Name = "b450", Location = "https://asset.msi.com/resize/image/global/product/product_1_20190716132640_5d2d60107d8bc.png62405b38c58fe0f07fcef2367d8a9ba1/1024.png" } },
                    LastUpdateDate = DateTime.Now,
                    CategoryId = 1,
                    Price = 25.99m,
                    PriceBeforeDiscount = 26.99m,
                    IsPublic = true,
                    IsRemoved = false
                },
                new Product
                {
                    Id = 2,
                    Name = "GTX 970 100ME",
                    CareationDate = DateTime.Today,
                    Description = "Ładna karta",
                    Images = new List<Image>{new Image() { Id = 2, Name = "970_100me", Location = "https://asset.msi.com/resize/image/global/product/five_pictures6_3342_20150129174155.png62405b38c58fe0f07fcef2367d8a9ba1/1024.png" } },
                    LastUpdateDate = DateTime.Now,
                    CategoryId = 1,
                    Price = 50000.99m,
                    PriceBeforeDiscount = 600006.99m,
                    IsPublic = true,
                    IsRemoved = false
                },
                new Product
                {
                    Id = 1,
                    Name = "B450 GAMING PLUS MAX",
                    CareationDate = DateTime.Today,
                    Description = "Ładna płyta",
                    Images = new List<Image>{new Image() { Id = 1, Name = "b450", Location = "https://asset.msi.com/resize/image/global/product/product_1_20190716132640_5d2d60107d8bc.png62405b38c58fe0f07fcef2367d8a9ba1/1024.png" } },
                    LastUpdateDate = DateTime.Now,
                    CategoryId = 1,
                    Price = 25.99m,
                    PriceBeforeDiscount = 26.99m,
                    IsPublic = true,
                    IsRemoved = false
                },
                new Product
                {
                    Id = 2,
                    Name = "GTX 970 100ME",
                    CareationDate = DateTime.Today,
                    Description = "Ładna karta",
                    Images = new List<Image>{new Image() { Id = 2, Name = "970_100me", Location = "https://asset.msi.com/resize/image/global/product/five_pictures6_3342_20150129174155.png62405b38c58fe0f07fcef2367d8a9ba1/1024.png" } },
                    LastUpdateDate = DateTime.Now,
                    CategoryId = 1,
                    Price = 50000.99m,
                    PriceBeforeDiscount = 600006.99m,
                    IsPublic = true,
                    IsRemoved = false
                },
                new Product
                {
                    Id = 1,
                    Name = "B450 GAMING PLUS MAX",
                    CareationDate = DateTime.Today,
                    Description = "Ładna płyta",
                    Images = new List<Image>{new Image() { Id = 1, Name = "b450", Location = "https://asset.msi.com/resize/image/global/product/product_1_20190716132640_5d2d60107d8bc.png62405b38c58fe0f07fcef2367d8a9ba1/1024.png" } },
                    LastUpdateDate = DateTime.Now,
                    CategoryId = 1,
                    Price = 25.99m,
                    PriceBeforeDiscount = 26.99m,
                    IsPublic = true,
                    IsRemoved = false
                },
                new Product
                {
                    Id = 2,
                    Name = "GTX 970 100ME",
                    CareationDate = DateTime.Today,
                    Description = "Ładna karta",
                    Images = new List<Image>{new Image() { Id = 2, Name = "970_100me", Location = "https://asset.msi.com/resize/image/global/product/five_pictures6_3342_20150129174155.png62405b38c58fe0f07fcef2367d8a9ba1/1024.png" } },
                    LastUpdateDate = DateTime.Now,
                    CategoryId = 1,
                    Price = 50000.99m,
                    PriceBeforeDiscount = 600006.99m,
                    IsPublic = true,
                    IsRemoved = false
                },
            };
        }
    }
}
