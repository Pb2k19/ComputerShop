using ComputerShop.Server.Services.User;
using ComputerShop.Shared.Models;
using ComputerShop.Shared.Models.User;
using System.Security.Claims;

namespace ComputerShop.Server.Services.WishList
{
    public class WishListService : IWishListService
    {
        private readonly IUserService userService;

        public WishListService(IUserService userService)
        {
            this.userService = userService;
        }

        public async Task<SimpleServiceResponse> AddToWishListAsync(string productId)
        {
            string? userId = userService.GetUserId();
            if (userId == null)
                return new SimpleServiceResponse { Message = "Coś poszło nie tak", Success = false };
            RegisteredUser? user = await userService.GetRegisteredUserByIdAsync(userId);
            if (user == null)
                return new SimpleServiceResponse { Message = "Coś poszło nie tak", Success = false };
            if (user.WishList.AddProduct(productId))
                return new SimpleServiceResponse();
            else
                return new SimpleServiceResponse { Message = "Nie można dodać produktu", Success = false };
        }

        public async Task<ServiceResponse<WishListModel>> GetWishListAsync()
        {
            string? userId = userService.GetUserId();
            if (userId == null)
                return new ServiceResponse<WishListModel> { Message = "Coś poszło nie tak", Success = false };
            RegisteredUser? user = await userService.GetRegisteredUserByIdAsync(userId);
            if (user == null)
                return new ServiceResponse<WishListModel> { Message = "Coś poszło nie tak", Success = false };
            return new ServiceResponse<WishListModel> { Data = user.WishList };
        }

        public async Task<SimpleServiceResponse> RemoveFromWishListAsync(string productId)
        {
            string? userId = userService.GetUserId();
            if (userId == null)
                return new SimpleServiceResponse { Message = "Coś poszło nie tak", Success = false };
            RegisteredUser? user = await userService.GetRegisteredUserByIdAsync(userId);
            if (user == null)
                return new SimpleServiceResponse { Message = "Coś poszło nie tak", Success = false };
            if(user.WishList.RemoveProduct(productId))
                return new SimpleServiceResponse();
            else
                return new SimpleServiceResponse { Message = "Nie można usunąć produktu", Success = false};
        }
    }
}
