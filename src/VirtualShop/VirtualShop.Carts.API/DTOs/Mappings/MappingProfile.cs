using AutoMapper;
using VirtualShop.Carts.API.Models;

namespace VirtualShop.Carts.API.DTOs.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CartDTO, Cart>().ReverseMap();
            CreateMap<CartHeaderDTO, CartHeader>().ReverseMap();
            CreateMap<CartItemDTO, CartItem>().ReverseMap();
            CreateMap<ProductDTO, CartItem>().ReverseMap();
        }
    }
}