using AutoMapper;
using VirtualShop.Discount.API.Models;

namespace VirtualShop.Discount.API.DTOs.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CouponDTO, Coupon>()
                .ReverseMap();
        }
    }
}