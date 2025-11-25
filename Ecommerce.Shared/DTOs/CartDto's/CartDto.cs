using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Shared.DTOs.CartDto_s
{
    public class CartDto
    {
        public string Id { get; set; }
        public ICollection<CartItemDto> Items { get; set; }

    }
}
