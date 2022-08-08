using VirtualShop.Discount.API.DTOs;

namespace VirtualShop.Discount.API.Repositories.Contracts
{
    public interface ICouponRepository
    {
        Task<CouponDTO> GetCouponByCode(string couponCode);     
    }
}