using AutoMapper;
using Ecommerce.Domain.Models.Products;
using Ecommerce.Shared.DTOs.ProductDro_s;
using Microsoft.Extensions.Configuration;
using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Service.MappingProfiles
{
    public class PictureUrlResolver(IConfiguration configuration) : IValueResolver<Product, ProductDto, string>
    {
        public string Resolve(Product source, ProductDto destination, string destMember, ResolutionContext context)
        {
            if(string.IsNullOrEmpty(source.PictureUrl)) return string.Empty;
            else
            {
                var url = $"{configuration.GetSection("Urls")["BaseURL"]}{source.PictureUrl}";
                return url;
            }
        }
    }
}
