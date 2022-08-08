using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VirtualShop.Discount.API.Contexts;
using VirtualShop.Discount.API.DTOs;
using VirtualShop.Discount.API.Repositories.Contracts;

namespace VirtualShop.Discount.API.Repositories
{
    public class CouponRepository : ICouponRepository
    {
        private readonly AppDbContext context;
        private readonly IMapper mapper;

        public CouponRepository(AppDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<CouponDTO> GetCouponByCode(string couponCode)
        {
            var coupon = await context.Coupons.FirstOrDefaultAsync(c => c.CouponCode != null && c.CouponCode.Equals(couponCode));
            return mapper.Map<CouponDTO>(coupon);
        }
    }
}