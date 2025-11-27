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
                //.ForMember(dest => dest.TypeId, opt => opt.MapFrom(src => src.ProductType))
                //.ForMember(dest => dest.BrandId, opt => opt.MapFrom(src => src.ProductBrand))
                .ReverseMap();
        }
    }
}
