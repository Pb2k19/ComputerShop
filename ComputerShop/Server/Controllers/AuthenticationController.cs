using ComputerShop.Server.Services.Authentication;
using ComputerShop.Shared.Models;
using ComputerShop.Shared.Models.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
                string cookie = "__Secure-Fgp=" + response.Data.Item2 + "; SameSite=Strict; HttpOnly; Secure";
                HttpContext.Response.Headers.Add("Set-Cookie", cookie);                
            }
            return new ServiceResponse<string>{Data=response.Data.Item1, Message = response.Message, Success = response.Success};
        }

        [HttpPost("/register")]
        public async Task<ActionResult<ServiceResponse<string>>> Register(Register register)
        {
            return await authentication.Register(register);
        }
    }
}
