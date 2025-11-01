using Ecommerce.Shared.Common;
using Ecommerce.Shared.Common.Specification_Pattern_Enhancment;
using Ecommerce.Shared.DTOs.ProductDro_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Abstraction.Services
{
    public  interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetProductsAsync(ProductQueryPrams  productQueryPrams);
        Task<IEnumerable<BrandDto>> GetBrandsAsync();
        Task<IEnumerable<TypeDto>> GetTypesAsync();
        Task<ProductDto> GetProductByIdAsync(int id);

    }
}
