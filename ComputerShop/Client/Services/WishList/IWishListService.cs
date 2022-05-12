using ComputerShop.Shared.Models;
using ComputerShop.Shared.Models.User;

namespace ComputerShop.Client.Services.WishList
{
    public interface IWishListService
    {
        Task<ServiceResponse<WishListModel>> GetWishListAsync();
        Task<SimpleServiceResponse> AddToWishListAsync(string productId);
        Task<SimpleServiceResponse> RemoveFromWishListAsync(string productId);
    }
}
