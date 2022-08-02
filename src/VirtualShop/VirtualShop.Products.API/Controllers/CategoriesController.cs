using System.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VirtualShop.Products.API.DTOs;
using VirtualShop.Products.API.Roles;
using VirtualShop.Products.API.Services.Contracts;

namespace VirtualShop.Products.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService service;

        public CategoriesController(ICategoryService service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetCategories()
        {
            var categories = await service.GetCategories();

            if (categories is null)
                return NotFound("Categories not found.");
            
            return Ok(categories);
        }

        [HttpGet("products")]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetCategoriesProducts()
        {
            var categories = await service.GetCategoriesProducts();

            if (categories is null)
                return NotFound("Categories not found.");
            
            return Ok(categories);
        }

        [HttpGet("{id:int}", Name = "GetCategoryById")]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetCategoryById(int id)
        {
            var category = await service.GetCategoryById(id);

            if (category is null)
                return NotFound("Category not found.");
            
            return Ok(category);
        }

        [HttpPost]
        public async Task<ActionResult> AddCategory([FromBody] CategoryDTO categoryDTO)
        {
            if (categoryDTO is null)
                return BadRequest("Invalid data.");
            
            await service.AddCategory(categoryDTO);

            return new CreatedAtRouteResult("GetCategoryById", new { id = categoryDTO.Id });
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateCategory([FromBody] CategoryDTO categoryDTO, int id)
        {
            if (categoryDTO is null)
                return BadRequest("Invalid data.");
            
            if (!categoryDTO.Id.Equals(id))
                return BadRequest("Id mismatch.");
            
            await service.UpdateCategory(categoryDTO);

            return Ok(categoryDTO);
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = Role.Admin)]
        public async Task<ActionResult<CategoryDTO>> RemoveCategory(int id)
        {
            var category = await service.GetCategoryById(id);

            if (category is null)
                return NotFound("Category not found.");
            
            await service.RemoveCategory(id);

            return Ok(category);
        }
    }
}