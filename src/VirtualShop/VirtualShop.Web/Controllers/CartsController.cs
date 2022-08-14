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
        private readonly ICouponService couponService;

        private string GetUserId() => User.Claims.First(w => w.Type.Equals("sub")).Value;

        private async Task<CartViewModel?> GetCartByUser()
        {
            var cart = await cartService.GetCartByUserIdAsync(GetUserId());

            if (cart?.CartHeader is not null) {

                if (!string.IsNullOrEmpty(cart.CartHeader.CouponCode)) {
                    var coupon = await couponService.GetDiscountCoupon(cart.CartHeader.CouponCode);
                    if (coupon?.CouponCode is not null)
                        cart.CartHeader.Discount = coupon.Discount;
                }

                foreach (var item in cart.CartItems)
                    if (item.Product is not null)
                        cart.CartHeader.TotalAmount += (item.Product.Price * item.Quantity);
                
                cart.CartHeader.TotalAmount = cart.CartHeader.TotalAmount - (cart.CartHeader.TotalAmount * cart.CartHeader.Discount) / 100;
            }

            return cart;
        }

        public CartsController(ICartService cartService, ICouponService couponService)
        {
            this.cartService = cartService;
            this.couponService = couponService;
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
            var result = await cartService.RemoveItemFromCartAsync(id);

            return result ?
                RedirectToAction(nameof(Index)):
                View(id);
        }

        [HttpPost("ApplyCoupon")]
        public async Task<IActionResult> ApplyCoupon(CartViewModel cartViewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await cartService.ApplyCouponAsync(cartViewModel);

                if (result)
                    return RedirectToAction(nameof(Index));
            }

            return View();
        }

        [HttpPost("DeleteCoupon")]
        public async Task<IActionResult> DeleteCoupon()
        {
            var result = await cartService.RemoveCouponAsync(GetUserId());

            if (result)
                return RedirectToAction(nameof(Index));
            
            return View();
        }
    }
}