using ComputerShop.Shared.Models;

namespace ComputerShop.Client.Services
{
    public interface ICartService
    {
        public event Action OnUpdate;
        public Task<List<CartItem>> GetAllCartItemsAsync();
        public Task<SimpleServiceResponse> AddItemToCartAsync(CartItem item);
        public Task<SimpleServiceResponse> RemoveItemFromCartAsync(string productId);
        public Task<(decimal, int)> GetCartInfoAsync();
        public Task<List<ProductCartItem>> GetCartProductsAsync();
        public Task UpdateCartItemQuantityAsync(int quantity, string productId );
    }
}
