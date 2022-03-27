using ComputerShop.Shared.Models;
using Blazored.LocalStorage;

namespace ComputerShop.Client.Services
{
    public class CartService : ICartService
    {
        private readonly ILocalStorageService localStorageService;
        private readonly IProductsService productsService;

        public string Key => "shoppingCart";

        public event Action? OnUpdate;

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
            if (item.Quantity < 1)
                return;
            List<CartItem> cart = await OpenCartAsync();
            CartItem? cartItem = cart.FirstOrDefault(x => x.ProductId.Equals(item.ProductId));
            if (cartItem == null)
            {
                cart.Add(item);
            }
            else
            {
                cartItem.Quantity += item.Quantity;
            }
            await localStorageService.SetItemAsync(Key, cart);
            OnUpdate?.Invoke();
        }

        public async Task RemoveItemFromCartAsync(string productId)
        {
            List<CartItem> cart = await OpenCartAsync();
            CartItem? toRemove = cart.FirstOrDefault(x => x.ProductId == productId);
            if (toRemove == null)
                return;
            cart.Remove(toRemove);
            await localStorageService.SetItemAsync(Key, cart);
            OnUpdate?.Invoke();
        }

        public async Task<(decimal, int)> GetCartInfoAsync()
        {
            List<CartItem> cart = await OpenCartAsync();
            return (cart.Sum(x => x.Price * x.Quantity), cart.Sum(x => x.Quantity));
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

        public async Task<List<ProductCartItem>> GetCartProductsAsync()
        {
            List<CartItem> cart = await OpenCartAsync();
            List<Task> tasks = new();
            List<ProductCartItem> result = new();
            foreach (var item in cart)
            {
                tasks.Add(Task.Run(async () =>
                {
                    var prod = await productsService.GetProductByIdAsync(item.ProductId);
                    if (prod != null)
                        result.Add(new ProductCartItem { Product = prod, Quantity = item.Quantity});
                }));
            }
            await Task.WhenAll(tasks);
            return result;
        }

        public async Task UpdateCartItemQuantityAsync(int quantity, string productId)
        {
            List<CartItem> cart = await OpenCartAsync();
            CartItem? cartItem = cart.FirstOrDefault(x => x.ProductId == productId);
            if (cartItem == null)
                return;
            cartItem.Quantity = quantity;
            await localStorageService.SetItemAsync(Key, cart);
            OnUpdate?.Invoke();
        }
    }
}
