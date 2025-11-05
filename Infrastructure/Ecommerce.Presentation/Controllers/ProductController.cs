using Ecommerce.Abstraction.Services;
using Ecommerce.Shared.Common;
using Ecommerce.Shared.Common.Pagination_Result;
using Ecommerce.Shared.Common.Specification_Pattern_Enhancment;
using Ecommerce.Shared.DTOs.ProductDro_s;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Presentation.Controllers
{
    [ApiController]
    [Route("api/[Controller]")] //routing el endpoint
    public class ProductController(IServiceManger manger) : ControllerBase
    {

        // [Controller] ➜ [Service/Handler] ➜ [Specification] ➜ [Repository]   ➜ [EF Core Query Execution]
        //



        //get all products 
        [HttpGet]
                                                                            //! 3shan bnst5dm complex query parameters
        public async Task<ActionResult<PaginationResult<ProductDto>>> GetAllProducts([FromQuery]ProductQueryPrams productQueryPrams)
        {
            var products = await manger.ProductServices.GetProductsAsync(productQueryPrams);
            return Ok(products);//hywl el data as json with the 200 status code
        }

        [HttpGet("Brands")]
        public async Task<ActionResult<IEnumerable<ProductBrandDto>>> GetAllProductBrands()
        {
            var brands = await manger.ProductServices.GetBrandsAsync();
            return Ok(brands);//hywl el data as json with the 200 status code
        }
        [HttpGet("Types")]
        public async Task<ActionResult<IEnumerable<TypeDto>>> GetAllProductTypes()
        {
            var types = await manger.ProductServices.GetTypesAsync();
            return Ok(types);//hywl el data as json with the 200 status code
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProductById(int id)
        {
            var product = await manger.ProductServices.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound(); // 404 if not found
            }
            return Ok(product);//hywl el data as json with the 200 status code
        }

    }
}
