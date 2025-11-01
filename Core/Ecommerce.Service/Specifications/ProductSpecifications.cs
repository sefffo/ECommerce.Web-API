using Ecommerce.Domain.Models.Products;
using Ecommerce.Shared.Common;
using Ecommerce.Shared.Common.Specification_Pattern_Enhancment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Service.Specifications
{
    public class ProductSpecifications : BaseSpecifications<Product, int>
    {                                       //specification for product entity

        //the base constructor needs an expression to filter by (Where)
        public ProductSpecifications(ProductQueryPrams productQueryPrams) : base
            (
                // hy3ml filter 3la 7asab el brand wla el type lw mawgodin
                p => (!productQueryPrams.BrandId.HasValue || p.BrandId == productQueryPrams.BrandId) &&
                (!productQueryPrams.TypeId.HasValue || p.TypeId == productQueryPrams.TypeId) &&
                (string.IsNullOrEmpty(productQueryPrams.SearchValue)||p.Name.ToLower().Contains(productQueryPrams.SearchValue.ToLower()))

            )
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);
            //each time we make a specification we extend the base specifications
            //and craete its own one by adding includes , criterias , order bys , paginations ...

            switch (productQueryPrams.SortingWay)
            {
                case ProductSortingWay.priceAsc:
                    AddOrderBy(p => p.Price);
                    break;
                case ProductSortingWay.priceDesc:
                    AddOrderByDesc(p => p.Price);
                    break;
                case ProductSortingWay.nameAsc:
                    AddOrderBy(p => p.Name);
                    break;
                case ProductSortingWay.nameDesc:
                    AddOrderByDesc(p => p.Name);
                    break;
                default:
                    break;
            }

        }
        public ProductSpecifications(int id) : base(p => p.Id == id)
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);
            //each time we make a specification we extend the base specifications
            //and craete its own one by adding includes , criterias , order bys , paginations ...
        }
    }
}
