using AutoMapper;
using Ecommerce.Domain.Models.Products;
using Ecommerce.Shared.DTOs.ProductDro_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Service.MappingProfiles
{
    public class ProjectProfiles : Profile
    {
        public ProjectProfiles()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(dist => dist.BrandName,
                            options => options.MapFrom(src => src.ProductBrand.Name))
                .ForMember(dist => dist.TypeName,
                            options => options.MapFrom(src => src.ProductType.Name));

            CreateMap<ProductBrand, BrandDto>();
            CreateMap<ProductType, TypeDto>();

        }
    }
}
