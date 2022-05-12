using ComputerShop.Server.Services.User;
using ComputerShop.Server.Services.WishList;
using ComputerShop.Shared.Models;
using ComputerShop.Shared.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ComputerShop.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishListController : ControllerBase
    {
        private readonly IUserService authentication;
        private readonly IWishListService wishListService;
        public WishListController(IUserService authentication, IWishListService wishListService)
        {
            this.authentication = authentication;
            this.wishListService = wishListService;
        }

        [HttpGet("getWishList"), Authorize]
        public async Task<ActionResult<ServiceResponse<WishListModel>>> GetWishList()
        {
            SimpleServiceResponse response = authentication.ValidateJWT(Request);
            if (!response.Success)
                return Unauthorized(response);
            Claim? userId = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userId == null)
                return new ServiceResponse<WishListModel>{ Message = "Nie można zweryfikować użytkownika", Success = false };
            
            return Ok(await wishListService.GetWishListAsync());
        }

        [HttpGet("addToWishList/{productId}"), Authorize]
        public async Task<ActionResult<SimpleServiceResponse>> AddToWishList([FromRoute]string productId)
        {
            SimpleServiceResponse response = authentication.ValidateJWT(Request);
            if (!response.Success)
                return Unauthorized(response);
            Claim? userId = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userId == null)
                return new SimpleServiceResponse { Message = "Nie można zweryfikować użytkownika", Success = false };

            return Ok(await wishListService.AddToWishListAsync(productId));
        }

        [HttpGet("removeFromWishList/{productId}"), Authorize]
        public async Task<ActionResult<SimpleServiceResponse>> RemoveFromWishList([FromRoute] string productId)
        {
            SimpleServiceResponse response = authentication.ValidateJWT(Request);
            if (!response.Success)
                return Unauthorized(response);
            Claim? userId = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userId == null)
                return new SimpleServiceResponse { Message = "Nie można zweryfikować użytkownika", Success = false };

            return Ok(await wishListService.RemoveFromWishListAsync(productId));
        }
    }
}
