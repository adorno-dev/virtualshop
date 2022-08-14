using VirtualShop.Web.Models;

namespace VirtualShop.Web.Services.Contracts
{
    public interface ICouponService
    {
        Task<CouponViewModel?> GetDiscountCoupon(string couponCode);        
    }
}