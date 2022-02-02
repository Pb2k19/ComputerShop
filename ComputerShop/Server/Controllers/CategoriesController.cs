using ComputerShop.Server.Services;
using ComputerShop.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ComputerShop.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoriesService categoryService;
        public CategoriesController(ICategoriesService categoryService)
        {
            this.categoryService = categoryService;
        }
        [HttpGet]
        public async Task<ActionResult<List<Category>>> GetCategoriesAsync()
        {
            return Ok(await categoryService.GetCategoriesAsync());
        }
        [HttpGet("getCategoryById/{id}")]
        public async Task<ActionResult<Category>> GetCategoryByIdAsync([FromRoute] int id)
        {
            return Ok(await categoryService.GetCategoryByIdAsync(id));
        }
        [HttpGet("getCategoryByUrl/{url}")]
        public async Task<ActionResult<Category>> GetCategoryByIdAsync([FromRoute] string url)
        {
            return Ok(await categoryService.GetCategoryByUrlAsync(url));
        }
    }
}
