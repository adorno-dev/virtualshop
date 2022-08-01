using AutoMapper;
using VirtualShop.Products.API.Models;

namespace VirtualShop.Products.API.DTOs.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<Product, ProductDTO>()
                .ForMember(p => p.CategoryName, c => c.MapFrom(src => src.Category.Name));
        }
    }
}