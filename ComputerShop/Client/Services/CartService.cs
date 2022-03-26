using ComputerShop.Shared.Models;
using Blazored.LocalStorage;

namespace ComputerShop.Client.Services
{
    public class CartService : ICartService
    {
        private readonly ILocalStorageService localStorageService;
        private readonly IProductsService productsService;

        public string Key => "shoppingCart";

        public event Action OnUpdate;

        public CartService(ILocalStorageService localStorageService, IProductsService productsService)
        {
            this.localStorageService = localStorageService;
            this.productsService = productsService;
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
            CartItem? toRemove = cart.FirstOrDefault(x => x.ProductId == item.ProductId);
            if (toRemove == null)
                return;
            cart.Remove(toRemove);
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

        public async Task<List<Product>> GetCartProductsAsync()
        {
            List<CartItem> cart = await OpenCartAsync();
            List<Task> tasks = new List<Task>();
            List<Product> result = new();
            foreach (var item in cart)
            {
                tasks.Add(Task.Run(async () =>
                {
                    var prod = await productsService.GetProductByIdAsync(item.ProductId);
                    if (prod != null)
                        result.Add(prod);
                }));
            }
            await Task.WhenAll(tasks);
            return result;
        }
    }
}
