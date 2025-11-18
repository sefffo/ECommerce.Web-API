using AutoMapper;
using Ecommerce.Domain.Models.Cart;
using Ecommerce.Domain.Models.Identity;
using Ecommerce.Domain.Models.Orders;
using Ecommerce.Domain.Models.Products;
using Ecommerce.Shared.DTOs.CartDto_s;
using Ecommerce.Shared.DTOs.IdentityDto_s;
using Ecommerce.Shared.DTOs.OrderDto_s;
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

            CreateMap<UserCart, CartDto>().ReverseMap();
            CreateMap<CartItem, CartItemDto>().ReverseMap(); //must be done because it happens internally inside of teh usercart to cartdto mapping 

            CreateMap<Address,AddressDto>().ReverseMap();


            CreateMap<AddressDto, OrderAddress>().ReverseMap();

            CreateMap<Order, OrderToReturnDto>().ForMember(d => d.DeliveryMethod, options => options.MapFrom(src => src.DeliveryMethod.ShortName)); //we wil not every reverse map this 


            CreateMap<OrderItem, OrderItemDto>().ForMember(src => src.ProductName, options => options.MapFrom(src => src.Product.ProductName))
                .ForMember(src => src.ProductPictureUrl, options => options.MapFrom(new orderPicResolver(configuration)));


            CreateMap<DeliveryMethod, DeliveryMethodDto>().ReverseMap();


        }
    }
}
