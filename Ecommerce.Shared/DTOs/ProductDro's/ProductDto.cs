using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Shared.DTOs.ProductDro_s
{
    public  class ProductDto
    {
        public int id { set; get; }
        public string Name { set; get; } = null!;

        public string Description { set; get; } = null!;
        public decimal Price { set; get; }
        public string PictureUrl { set; get; } = null!;
        public string BrandName { set; get; } = null!;

        public string TypeName { set; get; } = null!;
    }
}
