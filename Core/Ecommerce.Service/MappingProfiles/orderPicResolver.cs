using AutoMapper;
using Ecommerce.Domain.Models.Orders;
using Ecommerce.Shared.DTOs.OrderDto_s;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Service.MappingProfiles
{
    public class orderPicResolver(IConfiguration configuration) : IValueResolver<OrderItem, OrderItemDto, string>
    {
        public string Resolve(OrderItem source, OrderItemDto destination, string destMember, ResolutionContext context)
        {
            if (string.IsNullOrEmpty(source.Product.ProductImgUrl)) return string.Empty;
            else
            {
                var url = $"{configuration.GetSection("Urls")["BaseURL"]}{source.Product.ProductImgUrl}";
                return url;
            }
        }
    }
}
