using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VirtualShop.Web.Models;
using VirtualShop.Web.Services.Contracts;

namespace VirtualShop.Web.Controllers
{
    [Route("[controller]")]
    public class CartsController : Controller
    {
        private readonly ICartService cartService;

        private async Task<string> GetAccessToken() => await HttpContext.GetTokenAsync("access_token") ?? "";

        private string GetUserId() => User.Claims.First(w => w.Type.Equals("sub")).Value;

        private async Task<CartViewModel?> GetCartByUser()
        {
            var cart = await cartService.GetCartByUserIdAsync(GetUserId(), await GetAccessToken());

            if (cart?.CartHeader is not null) {
                foreach (var item in cart.CartItems)
                    if (item.Product is not null)
                        cart.CartHeader.TotalAmount += (item.Product.Price * item.Quantity);
            }

            return cart;
        }

        public CartsController(ICartService cartService)
        {
            this.cartService = cartService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var cartViewModel = await GetCartByUser();

            if (cartViewModel is not null)
                return View(cartViewModel);
            
            ModelState.AddModelError("CartNotFound", "Cart does not exist. Come on shopping...");

            return View("/Views/Carts/CartNotFound.cshtml");
        }

        [HttpGet("RemoveItem/{id:int}")]
        [Authorize]
        public async Task<IActionResult> RemoveItem(int id)
        {
            var result = await cartService.RemoveItemFromCartAsync(id, await GetAccessToken());

            return result ?
                RedirectToAction(nameof(Index)):
                View(id);
        }
    }
}