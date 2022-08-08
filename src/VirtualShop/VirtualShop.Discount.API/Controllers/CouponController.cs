using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VirtualShop.Discount.API.DTOs;
using VirtualShop.Discount.API.Repositories.Contracts;

namespace VirtualShop.Discount.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CouponController : ControllerBase
    {
        private ICouponRepository repository;

        public CouponController(ICouponRepository repository)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        [HttpGet("{couponCode}")]
        [Authorize]
        public async Task<ActionResult<CouponDTO>> GetDiscountCouponByCode(string couponCode)
        {
            var coupon = await repository.GetCouponByCode(couponCode);

            if (coupon is null)
                return NotFound($"Coupon Code: {couponCode} not found.");
            
            return Ok(coupon);
        }
    }
}