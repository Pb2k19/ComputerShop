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

        [HttpPost("/register")]
        public async Task<ActionResult<ServiceResponse<string>>> Register(Register register)
        {
            return await authentication.Register(register);
        }

    }
}
