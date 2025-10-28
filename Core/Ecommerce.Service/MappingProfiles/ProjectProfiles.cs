using AutoMapper;
using Ecommerce.Domain.Models.Products;
using Ecommerce.Shared.DTOs.ProductDro_s;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace Ecommerce.Service.MappingProfiles
{
    public class ProjectProfiles: Profile
    {
        public ProjectProfiles(IConfiguration configuration)
        {
            CreateMap<Product, ProductDto>()
                .ForMember(dist => dist.BrandName,
                            options => options.MapFrom(src => src.ProductBrand.Name))
                .ForMember(dist => dist.TypeName,
                            options => options.MapFrom(src => src.ProductType.Name))
                .ForMember(dist=>dist.PictureUrl , options=>options.MapFrom(new PictureUrlResolver(configuration)));

            CreateMap<ProductBrand, BrandDto>();
            CreateMap<ProductType, TypeDto>();

        }
    }
}
