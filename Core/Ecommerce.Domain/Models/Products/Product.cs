using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Domain.Models.Products
{
    public class Product:BaseEntity<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string PictureUrl { get; set; }
        public decimal Price { get; set; }


        public virtual ProductType ProductType { get; set; }
        public int TypeId { get; set; }


        public virtual ProductBrand ProductBrand { get; set; }
        public int BrandId { get; set; }

    }
}
