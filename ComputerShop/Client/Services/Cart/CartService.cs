using Blazored.LocalStorage;
using ComputerShop.Shared.Models;
using ComputerShop.Client.Services.Products;
using ComputerShop.Shared.Models.User;

namespace ComputerShop.Client.Services.Cart
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

        public async Task<SimpleServiceResponse> AddItemToCartAsync(CartItem item)
        {
            if (item == null || string.IsNullOrWhiteSpace(item.ProductId) || item.Quantity < 1)
                return new SimpleServiceResponse { Message="Nie udało się dodać produktu do koszyka", Success = false};
            List<CartItem> cart = await OpenCartAsync();
            CartItem? cartItem = cart.FirstOrDefault(x => x?.ProductId?.Equals(item?.ProductId) ?? false);
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
            return new SimpleServiceResponse { Success = true };
        }

        public async Task<SimpleServiceResponse> RemoveItemFromCartAsync(string productId)
        {
            if(string.IsNullOrWhiteSpace(productId))
            {
                return new SimpleServiceResponse { Message = "Nie udało się usunąć produktu z koszyka", Success = false };
            }
            List<CartItem> cart = await OpenCartAsync();
            CartItem? toRemove = cart.FirstOrDefault(x => x.ProductId.Equals(productId));
            if (toRemove == null)
                return new SimpleServiceResponse { Message = "Nie udało się usunąć produktu z koszyka", Success = false };
            cart.Remove(toRemove);
            await localStorageService.SetItemAsync(Key, cart);
            OnUpdate?.Invoke();
            return new SimpleServiceResponse { Success = true };
        }

        public async Task<(decimal, int)> GetCartInfoAsync()
        {
            List<CartItem> cart = await OpenCartAsync();
            return GetCartInfoAsync(cart);
        }

        public (decimal, int) GetCartInfoAsync(List<CartItem> cart)
        {
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
            List<Product> products = await productsService.GetProductsByIdListAsync(cart.Select(x => x.ProductId).ToList());
            List<ProductCartItem> items = new();
            products.ForEach(
                p => items.Add(new ProductCartItem 
                { 
                    Product = p, 
                    Quantity = cart.FirstOrDefault(c => c.ProductId.Equals(p.Id))?.Quantity ?? 1 
                }));
            await UpdateProductPrice(products, cart);
            return items;
        }

        public async Task UpdateProductPrice(List<Product> products, List<CartItem>? cart = null)
        {
            if (cart == null)
            {
                cart = await OpenCartAsync();
                if (cart == null)
                    return;
            }
            decimal priceBeforeUpdate = GetCartInfoAsync(cart).Item1;
            cart.ForEach(c => c.Price = products.FirstOrDefault(p => p.Id?.Equals(c.ProductId) ?? false)?.Price ?? c.Price);
            decimal priceAfterUpdate = GetCartInfoAsync(cart).Item1;
            if(priceBeforeUpdate != priceAfterUpdate)
            {
                await localStorageService.SetItemAsync(Key, cart);
                OnUpdate?.Invoke();
            }
        }

        public async Task UpdateCartItemQuantityAsync(int quantity, string productId)
        {
            List<CartItem> cart = await OpenCartAsync();
            CartItem? cartItem = cart.FirstOrDefault(x => x.ProductId.Equals(productId));
            if (cartItem == null)
                return;
            cartItem.Quantity = quantity;
            await localStorageService.SetItemAsync(Key, cart);
            OnUpdate?.Invoke();
        }
    }
}
