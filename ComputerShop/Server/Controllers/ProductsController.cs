using ComputerShop.Server.Services.Products;
using ComputerShop.Server.Services.User;
using ComputerShop.Shared.Models;
using ComputerShop.Shared.Models.Interfaces;
using ComputerShop.Shared.Models.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;

namespace ComputerShop.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsService productsService;
        private readonly IUserService userService;
        public ProductsController(IProductsService productsService, IUserService userService)
        {
            this.productsService = productsService;
            this.userService = userService;
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

        [HttpGet, Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<List<Product>>>> GetAllProducts()
        {
            SimpleServiceResponse response = userService.ValidateJWT(Request);
            if (!response.Success)
                return Unauthorized(new ServiceResponse<List<Product>> { Message = response.Message, Success = false });
            Claim? userId = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userId == null)
                return Unauthorized(new ServiceResponse<List<Product>> { Message = response.Message, Success = false });

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
            var isAdmin = User.IsInRole("Admin");
            if (isAdmin)
            {
                SimpleServiceResponse response = userService.ValidateJWT(Request);
                if (!response.Success)
                    return Unauthorized(new ServiceResponse<ProductsResponse> { Message = response.Message, Success = false });
                Claim? userId = User.FindFirst(ClaimTypes.NameIdentifier);
                if (userId == null)
                    return Unauthorized(new ServiceResponse<ProductsResponse> { Message = response.Message, Success = false });
            }

            ServiceResponse<ProductsResponse> serviceResponse = new()
            {
                Data = await productsService.GetProductsByCategoryAsync(category, page, sortFilterOptions, isAdmin)
            };
            return Ok(serviceResponse);
        }

        [HttpPost("find/{text}/{page}")]
        public async Task<ActionResult<ServiceResponse<ProductsResponse>>> FindProductsByTextAsync(ProductSortFilterOptions? sortFilterOptions, [FromRoute] string text, [FromRoute] int page = 1)
        {
            if (text.Length > 128)
                return BadRequest();
            var isAdmin = User.IsInRole("Admin");
            if(isAdmin)
            {
                SimpleServiceResponse response = userService.ValidateJWT(Request);
                if (!response.Success)
                    return Unauthorized(new ServiceResponse<ProductsResponse> { Message = response.Message, Success = false });
                Claim? userId = User.FindFirst(ClaimTypes.NameIdentifier);
                if (userId == null)
                    return Unauthorized(new ServiceResponse<ProductsResponse> { Message = response.Message, Success = false });
            }

            ServiceResponse<ProductsResponse> serviceResponse = new()
            {
                Data = await productsService.FindProductsByTextAsync(text, page, sortFilterOptions, isAdmin)
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

        [HttpPost("addProduct"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<SimpleServiceResponse>> AddProduct(StringValue productJson, [FromRoute] string category)
        {
            SimpleServiceResponse response = userService.ValidateJWT(Request);
            if (!response.Success)
                return Unauthorized(new ServiceResponse<List<Product>> { Message = response.Message, Success = false });
            Claim? userId = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userId == null)
                return Unauthorized(new ServiceResponse<List<Product>> { Message = response.Message, Success = false });

            return Ok(await productsService.AddProductAsync(productJson.Value, category));
        }

        [HttpPost("editProduct/{category}"), Authorize(Roles ="Admin")]
        public async Task<ActionResult<ServiceResponse<Product>>> EditProduct(StringValue productJson,[FromRoute] string category)
        {
            SimpleServiceResponse response = userService.ValidateJWT(Request);
            if (!response.Success)
                return Unauthorized(new ServiceResponse<List<Product>> { Message = response.Message, Success = false });
            Claim? userId = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userId == null)
                return Unauthorized(new ServiceResponse<List<Product>> { Message = response.Message, Success = false });

            return Ok(await productsService.UpdateProductAsync(productJson.Value, category));
        }

        [HttpPost("addComment/{productId}"), Authorize]
        public async Task<ActionResult<ServiceResponse<Product>>> AddComment(Comment comment, [FromRoute] string productId)
        {
            SimpleServiceResponse response = userService.ValidateJWT(Request);
            if (!response.Success)
                return Unauthorized(new ServiceResponse<List<Product>> { Message = response.Message, Success = false });
            Claim? userId = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userId == null)
                return Unauthorized(new ServiceResponse<List<Product>> { Message = response.Message, Success = false });

            return Ok(await productsService.AddCommentToProductAsync(comment, productId));
        }
    }
}
