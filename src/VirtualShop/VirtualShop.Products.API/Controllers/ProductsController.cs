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
    public class ProductsController : ControllerBase
    {
        private readonly IProductService service;

        public ProductsController(IProductService service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProducts()
        {
            var products = await service.GetProducts();

            if (products is null)
                return NotFound("Products not found.");
            
            return Ok(products);
        }

        [HttpGet("{id:int}", Name = "GetProductById")]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProductById(int id)
        {
            var product = await service.GetProductById(id);

            if (product is null)
                return NotFound("Product not found.");
            
            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult> AddProduct([FromBody] ProductDTO productDTO)
        {
            if (productDTO is null)
                return BadRequest("Invalid data.");
            
            await service.AddProduct(productDTO);

            return new CreatedAtRouteResult("GetProductById", new { id = productDTO.Id });
        }

        [HttpPut]
        public async Task<ActionResult> UpdateProduct([FromBody] ProductDTO productDTO)
        {
            if (productDTO is null)
                return BadRequest("Invalid data.");
            
            await service.UpdateProduct(productDTO);

            return Ok(productDTO);
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = Role.Admin)]
        public async Task<ActionResult<ProductDTO>> RemoveProduct(int id)
        {
            var product = await service.GetProductById(id);

            if (product is null)
                return NotFound("Product not found.");
            
            await service.RemoveProduct(id);

            return Ok(product);
        }
    }
}