using ComputerShop.Shared.Models;
using ComputerShop.Shared.Models.User;
using System.Net.Http.Json;

namespace ComputerShop.Client.Services.WishList
{
    internal class WishListService : IWishListService
    {
        private readonly HttpClient httpClient;

        public WishListService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<SimpleServiceResponse> AddToWishListAsync(string productId)
        {
            var respone = await httpClient.GetFromJsonAsync<SimpleServiceResponse>($"api/wishlist/addToWishList/{productId}");
            return respone ?? new SimpleServiceResponse { Success = false, Message = "Coś poszło nie tak"};
        }

        public async Task<ServiceResponse<WishListModel>> GetWishListAsync()
        {
            var respone = await httpClient.GetFromJsonAsync<ServiceResponse<WishListModel>>("api/wishlist/getWishList");
            return respone ?? new ServiceResponse<WishListModel> { Success = false, Message = "Coś poszło nie tak" };
        }

        public async Task<SimpleServiceResponse> RemoveFromWishListAsync(string productId)
        {
            var respone = await httpClient.GetFromJsonAsync<SimpleServiceResponse>($"api/wishlist/removeFromWishList/{productId}");
            return respone ?? new SimpleServiceResponse { Success = false, Message = "Coś poszło nie tak" };
        }
    }
}
