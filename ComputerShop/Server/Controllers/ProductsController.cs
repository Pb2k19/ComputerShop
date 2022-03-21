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

        [HttpGet("getProductById/{id}")]
        public async Task<ActionResult<ServiceResponse<Product>>> GetProductByIdAsync([FromRoute] string id)
        {
            Product? product = await productsService.GetProductByIdAsync(id);
            ServiceResponse<object> serviceResponse = new();
            if (product == null)
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

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<Product>>>> GetProductsAll()
        {
            ServiceResponse<List<Product>> serviceResponse = new()
            {
                Data = await productsService.GetAllProductsAsync()
            };
            return Ok(serviceResponse);
        }

        [HttpGet("getHighlightedProduscts")]
        public async Task<ActionResult<ServiceResponse<List<Product>>>> GetHiglightedProducts()
        {
            ServiceResponse<List<Product>> serviceResponse = new()
            {
                Data = await productsService.GetHiglightedProductsAsync()
            };
            return Ok(serviceResponse);
        }

        [HttpGet("getByCategoryId/{id}/{page}")]
        public async Task<ActionResult<ServiceResponse<ProductsResponse>>> GetProductsByCategoryIdAndPageAsync([FromRoute] string id, [FromRoute] int page)
        {
            ServiceResponse<ProductsResponse> serviceResponse = new()
            {
                Data = await productsService.GetProductsByCategoryIdAsync(id, page)
            };
            return Ok(serviceResponse);
        }

        [HttpGet("getByUrl/{url}/{page}")]
        public async Task<ActionResult<ServiceResponse<ProductsResponse>>> GetProductsByCategoryUrlAsync([FromRoute] string url, [FromRoute] int page)
        {
            ServiceResponse<ProductsResponse> serviceResponse = new()
            {
                Data = await productsService.GetProductsByCategoryUrlAsync(url, page)
            };
            return Ok(serviceResponse);
        }

        [HttpGet("find/{text}/{page}")]
        public async Task<ActionResult<ServiceResponse<ProductsResponse>>> FindProductsByTextAsync([FromRoute] string text, [FromRoute] int page = 1)
        {
            ServiceResponse<ProductsResponse> serviceResponse = new()
            {
                Data = await productsService.FindProductsByTextAsync(text, page)
            };
            return Ok(serviceResponse);
        }
        [HttpGet("getProductSuggestions/{text}")]
        public async Task<ActionResult<ServiceResponse<List<string>>>> GetProductSuggestionsByTextAsync([FromRoute] string text)
        {
            ServiceResponse<List<string>> serviceResponse = new()
            {
                Data = await productsService.GetProductsSuggestionsByTextAsync(text)
            };
            return Ok(serviceResponse);
        }
    }
}
