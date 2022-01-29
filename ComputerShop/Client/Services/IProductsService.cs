using ComputerShop.Shared.Models;

namespace ComputerShop.Client.Services
{
    public interface IProductsService
    {
        List<Product> Products { get; set; }
        public void Load();
    }
}
