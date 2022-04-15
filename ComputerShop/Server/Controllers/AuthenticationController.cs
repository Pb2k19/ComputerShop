﻿using ComputerShop.Server.Services.Authentication;
using ComputerShop.Server.Helpers;
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
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService authentication;
        public AuthenticationController(IAuthenticationService authentication)
        {
            this.authentication = authentication;
        }

        [HttpPost("/login")]
        public async Task<ActionResult<ServiceResponse<string>>> Login(Login login)
        {
            var response = await authentication.Login(login);
            if (response.Success)
            {
                string time = DateTime.UtcNow.AddHours(24).ToString("ddd, dd MMM yyyy HH:mm:ss 'UTC'", CultureInfo.GetCultureInfo("en-US"));
                string cookie = $"__Secure-Fgp={response.Data.SecureFgpBase64}; SameSite=Strict; HttpOnly; Secure; Expires={time};";
                HttpContext.Response.Headers.Add("Set-Cookie", cookie);
            }
            return Ok(new ServiceResponse<string>
            {
                Data = response.Data?.TokenValue ?? string.Empty,
                Message = response.Message,
                Success = response.Success
            });
        }

        [HttpPost("/register")]
        public async Task<ActionResult<SimpleServiceResponse>> Register(Register register)
        {
            return Ok(await authentication.Register(register));
        }

        [HttpPost("/change-password"), Authorize]
        public async Task<ActionResult<SimpleServiceResponse>> ChangePassword(ChangePassword newPassword)
        {
            if(!authentication.ValidateJWT(Request, User.Identity))
                return Unauthorized(new SimpleServiceResponse { Message = "Nie można zweryfikować użytkownika", Success = false});
            Claim? userId = User.FindFirst(ClaimTypes.NameIdentifier);
            if(userId == null)
                return Unauthorized(new SimpleServiceResponse { Message = "Nie można zweryfikować użytkownika", Success = false });
            return Ok(await authentication.ChangePassword(userId.Value,newPassword));
        }
    }
}
