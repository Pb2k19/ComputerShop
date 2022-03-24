using ComputerShop.Shared.Models;
using Blazored.LocalStorage;

namespace ComputerShop.Client.Services
{
    public class CartService : ICartService
    {
        private readonly ILocalStorageService localStorageService;

        public string Key => "shoppingCart";

        public event Action OnUpdate;

        public CartService(ILocalStorageService localStorageService)
        {
            this.localStorageService = localStorageService;
        }

        public async Task<List<CartItem>> GetAllCartItemsAsync()
        {
            return await OpenCartAsync();
        }

        public async Task AddItemToCartAsync(CartItem item)
        {
            List<CartItem> cart = await OpenCartAsync();
            cart.Add(item);
            await localStorageService.SetItemAsync(Key, cart);
            OnUpdate?.Invoke();
        }

        public async Task RemoveItemFromCartAsync(CartItem item)
        {
            List<CartItem> cart = await OpenCartAsync();
            cart.Remove(item); //tmp
            await localStorageService.SetItemAsync(Key, cart);
            OnUpdate?.Invoke();
        }

        public async Task<decimal> GetCartValueAsync()
        {
            List<CartItem> cart = await OpenCartAsync();
            return cart.Sum(x => x.Price);
        }

        private async Task<List<CartItem>> OpenCartAsync()
        {
            List<CartItem>? cart = await localStorageService.GetItemAsync<List<CartItem>>(Key);
            if (cart == null)
            {
                cart = new();
            }
            return cart;
        }
    }
}
