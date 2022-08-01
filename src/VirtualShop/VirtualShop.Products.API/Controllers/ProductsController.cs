using Microsoft.AspNetCore.Mvc;
using VirtualShop.Products.API.DTOs;
using VirtualShop.Products.API.Services.Contracts;

namespace VirtualShop.Products.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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

        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateProduct([FromBody] ProductDTO productDTO, int id)
        {
            if (productDTO is null)
                return BadRequest("Invalid data.");
            
            if (!productDTO.Id.Equals(id))
                return BadRequest("Id mismatch.");
            
            await service.UpdateProduct(productDTO);

            return Ok(productDTO);
        }

        [HttpDelete("{id:int}")]
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