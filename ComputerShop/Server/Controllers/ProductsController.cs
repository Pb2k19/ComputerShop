using ComputerShop.Server.Services;
using ComputerShop.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ComputerShop.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsService productsService;
        public ProductsController(IProductsService productsService)
        {
            this.productsService = productsService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProductsAll()
        {
            return Ok(await productsService.GetAllProductsAsync());
        }

        [HttpGet("getByCategoryId/{id}")]
        public async Task<ActionResult<List<Product>>> GetProductsByCategoryIdAsync([FromRoute]int id)
        {
            return Ok(await productsService.GetProductsByCategoryIdAsync(id));
        }

        [HttpGet("getByUrl/{url}")]
        public async Task<ActionResult<List<Product>>> GetProductsByCategoryUrlAsync([FromRoute] string url)
        {
            return Ok(await productsService.GetProductsByCategoryUrlAsync(url));
        }
        [HttpGet("getProductById/{id}")]
        public async Task<ActionResult<Product>> GetProductByIdAsync([FromRoute] int id)
        {
            return Ok(await productsService.GetProductByIdAsync(id));
        }
    }
}
