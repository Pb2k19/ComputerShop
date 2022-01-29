using ComputerShop.Shared.Models;

namespace ComputerShop.Client.Services
{
    public interface ICategoryService
    {
        List<Category> Categories { get; set; }
        void Load();
    }
}
