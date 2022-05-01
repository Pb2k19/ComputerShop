using ComputerShop.Shared.Models;
using ComputerShop.Shared.Models.User;

namespace ComputerShop.Server.Services.WishList
{
    public interface IWishListService
    {
        ServiceResponse<WishLisModel> GetWishList();
        SimpleServiceResponse AddToWishList(string productId);
        SimpleServiceResponse RemoveFromWishList(string productId);

    }
}
