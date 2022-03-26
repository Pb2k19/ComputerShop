using ComputerShop.Shared.Models;

namespace ComputerShop.Client.Services
{
    public interface ICartService
    {
        public event Action OnUpdate;
        public Task<List<CartItem>> GetAllCartItemsAsync();
        public Task AddItemToCartAsync(CartItem item);
        public Task RemoveItemFromCartAsync(CartItem item);
        public Task<decimal> GetCartValueAsync();
        public Task<List<Product>> GetCartProductsAsync();
    }
}
