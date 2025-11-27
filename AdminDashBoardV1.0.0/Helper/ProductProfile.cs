using AutoMapper;
using Ecommerce.Domain.Models.Products;
using AdminDashBoardV1._0._0.Models;
namespace AdminDashBoardV1._0._0.Helper
{
    public class productProfile : Profile
    {
        public productProfile()
        {
            CreateMap<Product, ProductViewModel>()
                .ForMember(dest => dest.Brand, opt => opt.MapFrom(src => src.ProductBrand))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.ProductType))
                .ForMember(dest => dest.BrandId, opt => opt.MapFrom(src => src.BrandId))  // ✅ Fixed
                .ForMember(dest => dest.TypeId, opt => opt.MapFrom(src => src.TypeId))    // ✅ Fixed
                .ReverseMap()
                .ForMember(dest => dest.BrandId, opt => opt.MapFrom(src => src.BrandId))
                .ForMember(dest => dest.TypeId, opt => opt.MapFrom(src => src.TypeId));
        }
    }
}
