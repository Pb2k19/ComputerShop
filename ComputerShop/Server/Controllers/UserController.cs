﻿using ComputerShop.Server.Services.UserService;
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
        private readonly IUserService authentication;
        public UserController(IUserService authentication)
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

        [HttpPost("/changePassword"), Authorize]
        public async Task<ActionResult<SimpleServiceResponse>> ChangePassword(ChangePassword newPassword)
        {
            SimpleServiceResponse response = CheckAuthentication(User, Request);
            if (!response.Success)
                return Unauthorized(response);
            Claim? userId = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userId == null)
                return new SimpleServiceResponse { Message = "Nie można zweryfikować użytkownika", Success = false };
            return Ok(await authentication.ChangePassword(userId.Value,newPassword));
        }

        [HttpGet("/checkAuthentication")]
        public ActionResult<SimpleServiceResponse> CheckAuthentication()
        {
            SimpleServiceResponse response = CheckAuthentication(User, Request);
            if(response.Success)
                return Ok(response);
            else
                return Unauthorized(response);
        }

        private SimpleServiceResponse CheckAuthentication(ClaimsPrincipal user, HttpRequest request)
        {
            if (!authentication.ValidateJWT(request, user.Identity))
                return new SimpleServiceResponse { Message = "Nie można zweryfikować użytkownika", Success = false };
            else
                return new SimpleServiceResponse();
        }
    }
}