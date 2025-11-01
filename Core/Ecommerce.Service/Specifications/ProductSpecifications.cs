using Ecommerce.Domain.Models.Products;
using Ecommerce.Shared.Common;
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
        public ProductSpecifications(int? BrandId, int? TypeId, ProductSortingWay? sortingWay) : base
            (
                // hy3ml filter 3la 7asab el brand wla el type lw mawgodin
                p => (!BrandId.HasValue || p.BrandId == BrandId) &&
                (!TypeId.HasValue || p.TypeId == TypeId)
            )
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);
            //each time we make a specification we extend the base specifications
            //and craete its own one by adding includes , criterias , order bys , paginations ...

            switch (sortingWay)
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
