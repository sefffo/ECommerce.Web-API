using Ecommerce.Domain.Models.Products;
using Ecommerce.Shared.Common.Specification_Pattern_Enhancment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Service.Specifications
{
    public class ProductCountSpecification : BaseSpecifications<Product, int>
    {
        public ProductCountSpecification(ProductQueryPrams productQueryPrams) : base
            (
                // hy3ml filter 3la 7asab el brand wla el type lw mawgodin
                p => (!productQueryPrams.BrandId.HasValue || p.BrandId == productQueryPrams.BrandId) &&
                (!productQueryPrams.TypeId.HasValue || p.TypeId == productQueryPrams.TypeId) &&
                (string.IsNullOrEmpty(productQueryPrams.SearchValue) || p.Name.ToLower().Contains(productQueryPrams.SearchValue.ToLower()))

            )
        {
        }
    }
}
