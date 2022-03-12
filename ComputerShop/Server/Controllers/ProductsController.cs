using ComputerShop.Server.Services;
using ComputerShop.Shared.Models;
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
        public async Task<ActionResult<ServiceResponse<List<Product>>>> GetProductsAll()
        {
            ServiceResponse<List<Product>> serviceResponse = new()
            {
                Data = await productsService.GetAllProductsAsync()
            };
            return Ok(serviceResponse);
        }

        [HttpGet("getByCategoryId/{id}")]
        public async Task<ActionResult<ServiceResponse<List<Product>>>> GetProductsByCategoryIdAsync([FromRoute] int id)
        {
            ServiceResponse<List<Product>> serviceResponse = new()
            {
                Data = await productsService.GetProductsByCategoryIdAsync(id)
            };
            return Ok(serviceResponse);
        }

        [HttpGet("getByUrl/{url}")]
        public async Task<ActionResult<ServiceResponse<List<Product>>>> GetProductsByCategoryUrlAsync([FromRoute] string url)
        {
            ServiceResponse<List<Product>> serviceResponse = new()
            {
                Data = await productsService.GetProductsByCategoryUrlAsync(url)
            };
            return Ok(serviceResponse);
        }
        [HttpGet("getProductById/{id}")]
        public async Task<ActionResult<ServiceResponse<Product>>> GetProductByIdAsync([FromRoute] int id)
        {
            Product? product = await productsService.GetProductByIdAsync(id);
            ServiceResponse<object> serviceResponse = new();
            if(product == null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Nie znaleziono produktu";
            }
            else
            {
                serviceResponse.Data = product;
            }
            return Ok(serviceResponse);
        }
    }
}
