using ComputerShop.Server.Services.User;
using ComputerShop.Server.Services.UserDetails;
using ComputerShop.Shared.Models;
using ComputerShop.Shared.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Security.Claims;

namespace ComputerShop.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly IUserDetailsService userDetails;

        public UserController(IUserService authentication, IUserDetailsService userDetails)
        {
            this.userService = authentication;
            this.userDetails = userDetails;
        }

        [HttpPost("login")]
        public async Task<ActionResult<ServiceResponse<string>>> Login(Login login)
        {
            var response = await userService.LoginAsync(login);
            if (response.Success)
            {
                string time = DateTime.UtcNow.AddHours(24).ToString("ddd, dd MMM yyyy HH:mm:ss 'UTC'", CultureInfo.GetCultureInfo("en-US"));
                string cookie = $"__Secure-Fgp={response.Data.SecureFgpBase64}; SameSite=Strict; HttpOnly; Secure; Expires={time}; Path=/;";
                HttpContext.Response.Headers.Add("Set-Cookie", cookie);
            }
            return Ok(new ServiceResponse<string>
            {
                Data = response.Data?.TokenValue ?? string.Empty,
                Message = response.Message,
                Success = response.Success
            });
        }

        [HttpGet("logout"), Authorize]
        public ActionResult Logout()
        {
            string time = DateTime.UtcNow.ToString("ddd, dd MMM yyyy HH:mm:ss 'UTC'", CultureInfo.GetCultureInfo("en-US"));
            string cookie = $"__Secure-Fgp={string.Empty}; SameSite=Strict; HttpOnly; Secure; Expires={time}; Path=/;";
            HttpContext.Response.Headers.Add("Set-Cookie", cookie);
            return Ok();
        }

        [HttpPost("register")]
        public async Task<ActionResult<SimpleServiceResponse>> Register(Register register)
        {
            return Ok(await userService.RegisterAsync(register));
        }

        [HttpPost("changePassword"), Authorize]
        public async Task<ActionResult<SimpleServiceResponse>> ChangePassword(ChangePassword newPassword)
        {
            SimpleServiceResponse response = userService.ValidateJWT(Request);
            if (!response.Success)
                return Unauthorized(response);
            Claim? userId = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userId == null)
                return Unauthorized(new SimpleServiceResponse { Message = "Nie można zweryfikować użytkownika", Success = false });
            return Ok(await userService.ChangePasswordAsync(newPassword));
        }

        [HttpGet("checkAuthentication")]
        public ActionResult<SimpleServiceResponse> CheckAuthentication()
        {
            SimpleServiceResponse response = userService.ValidateJWT(Request);
            if (response.Success)
                return Ok(response);
            else
                return Unauthorized(response);
        }

        [HttpGet("getDeliveryDetails"), Authorize]
        public async Task<ActionResult<ServiceResponse<DeliveryDetails>>> GetDeliveryDetails()
        {
            SimpleServiceResponse response = userService.ValidateJWT(Request);
            if (!response.Success)
                return Unauthorized(response);
            Claim? userId = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userId == null)
                return Unauthorized(new ServiceResponse<DeliveryDetails> { Message = "Nie można zweryfikować użytkownika", Success = false });

            return Ok(await userDetails.GetDeliveryDetailsAsync());
        }

        [HttpGet("getInvoiceDetails"), Authorize]
        public async Task<ActionResult<ServiceResponse<InvoiceDetails>>> GetInvoiceDetails()
        {
            SimpleServiceResponse response = userService.ValidateJWT(Request);
            if (!response.Success)
                return Unauthorized(response);
            Claim? userId = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userId == null)
                return Unauthorized(new ServiceResponse<DeliveryDetails> { Message = "Nie można zweryfikować użytkownika", Success = false });

            return Ok(await userDetails.GetInvoiceDetailsAsync());
        }

        [HttpPost("updateDeliveryDetails")]
        public async Task<ActionResult<SimpleServiceResponse>> UpdateDeliveryDetails(DeliveryDetails delivery)
        {
            SimpleServiceResponse response = userService.ValidateJWT(Request);
            if (!response.Success)
                return Unauthorized(response);
            Claim? userId = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userId == null)
                return Unauthorized(new SimpleServiceResponse { Message = "Nie można zweryfikować użytkownika", Success = false });

            return Ok(await userDetails.UpdateDeliveryDetailsAsync(delivery));
        }

        [HttpPost("updateInvoiceDetails")]
        public async Task<ActionResult<SimpleServiceResponse>> UpdateInvoiceDetails(InvoiceDetails invoice)
        {
            SimpleServiceResponse response = userService.ValidateJWT(Request);
            if (!response.Success)
                return Unauthorized(response);
            Claim? userId = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userId == null)
                return Unauthorized(new SimpleServiceResponse { Message = "Nie można zweryfikować użytkownika", Success = false });

            return Ok(await userDetails.UpdateInvoiceDetailsAsync(invoice));
        }

        [HttpGet("getAllUsers"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<List<UserModel>>>> GetAllUsers()
        {
            SimpleServiceResponse response = userService.ValidateJWT(Request);
            if (!response.Success)
                return Unauthorized(response);
            Claim? userId = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userId == null)
                return Unauthorized(new SimpleServiceResponse { Message = "Nie można zweryfikować użytkownika", Success = false });
            ServiceResponse<List<UserModel>> users = new() { Data = await userService.GetAllUsersAsync(), Success = true };
            return Ok(users);
        }
    }
}