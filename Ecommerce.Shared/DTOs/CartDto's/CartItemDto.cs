using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Shared.DTOs.CartDto_s
{
    public class CartItemDto
    {
        public int Id { set; get; }
        public string Name { set; get; } = null!;
        public string PictureUrl { set; get; } = null!;
        [Range(1,double.MaxValue)]
        public decimal Price { set; get; }
        [Range(1, 100)]
        public int Quantity { set; get; }
    }
}
