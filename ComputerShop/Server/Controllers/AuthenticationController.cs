using ComputerShop.Server.Services.Authentication;
using ComputerShop.Shared.Models;
using ComputerShop.Shared.Models.User;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

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
            if(response.Success)
            {
                string time = DateTime.Now.AddHours(24).ToString("ddd, dd MMM yyyy HH:mm:ss 'GMT'", CultureInfo.GetCultureInfo("en-US"));
                string cookie = $"__Secure-Fgp={response.Data.SecureFgpBase64}; SameSite=Strict; HttpOnly; Secure; Expires={time};";
                HttpContext.Response.Headers.Add("Set-Cookie", cookie);                
            }
            return new ServiceResponse<string>{Data=response.Data?.TokenValue ?? string.Empty, Message = response.Message, Success = response.Success};
        }

        [HttpPost("/register")]
        public async Task<ActionResult<ServiceResponse<string>>> Register(Register register)
        {
            return await authentication.Register(register);
        }
    }
}
