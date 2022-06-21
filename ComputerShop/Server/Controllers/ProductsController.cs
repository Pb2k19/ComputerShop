using ComputerShop.Server.Services.Products;
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

        [HttpPost("getProductsByIdList")]
        public async Task<ActionResult<ServiceResponse<Product>>> GetProductByIdAsync(List<string> idList)
        {
            var products = await productsService.GetProductsByIdListAsync(idList);
            ServiceResponse<object> serviceResponse = new();
            if (products.Count == 0)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Nie znaleziono produktów";
            }
            else
            {
                serviceResponse.Data = products;
            }
            return Ok(serviceResponse);
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<Product>>>> GetAllProducts()
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
                Data = await productsService.GetHighlightedProductsAsync()
            };
            return Ok(serviceResponse);
        }

        [HttpPost("getByCategory/{category}/{page}")]
        public async Task<ActionResult<ServiceResponse<ProductsResponse>>> GetProductsByCategoryUrlAsync(ProductSortFilterOptions? sortFilterOptions, [FromRoute] string category, [FromRoute] int page)
        {
            ServiceResponse<ProductsResponse> serviceResponse = new()
            {
                Data = await productsService.GetProductsByCategoryAsync(category, page, sortFilterOptions)
            };
            return Ok(serviceResponse);
        }

        [HttpPost("find/{text}/{page}")]
        public async Task<ActionResult<ServiceResponse<ProductsResponse>>> FindProductsByTextAsync(ProductSortFilterOptions? sortFilterOptions, [FromRoute] string text, [FromRoute] int page = 1)
        {
            if (text.Length > 128)
                return BadRequest();
            ServiceResponse<ProductsResponse> serviceResponse = new()
            {
                Data = await productsService.FindProductsByTextAsync(text, page, sortFilterOptions)
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
