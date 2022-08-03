using Microsoft.AspNetCore.Mvc;
using VirtualShop.Carts.API.DTOs;
using VirtualShop.Carts.API.Repositories.Contracts;

namespace VirtualShop.Carts.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private ICartRepository repository;

        public CartController(ICartRepository repository)
        {
            this.repository = repository ?? 
                throw new ArgumentNullException(nameof(repository));
        }

        [HttpGet("GetCart/{userId}")]
        public async Task<ActionResult<CartDTO>> GetByUserId(string userId)
        {
            return await repository.GetCartByUserIdAsync(userId) is CartDTO cart ?
                Ok(cart):
                NotFound();
        }

        [HttpPost("AddCart")]
        public async Task<ActionResult<CartDTO>> AddCart(CartDTO cartDTO)
        {
            return await repository.UpdateCartAsync(cartDTO) is CartDTO cart ?
                Ok(cart):
                NotFound();
        }

        [HttpPut("UpdateCart")]
        public async Task<ActionResult<CartDTO>> UpdateCart(CartDTO cartDTO)
        {
            return await repository.UpdateCartAsync(cartDTO) is CartDTO cart ?
                Ok(cart):
                NotFound();
        }

        [HttpDelete("DeleteCart/{id:int}")]
        public async Task<ActionResult<bool>> DeleteCart(int id)
        {
            return await repository.DeleteItemCartAsync(id) is true ?
                Ok(true):
                BadRequest(false);
        }
    }
}