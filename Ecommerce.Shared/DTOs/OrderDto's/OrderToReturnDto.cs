using Ecommerce.Shared.DTOs.IdentityDto_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Shared.DTOs.OrderDto_s
{
    public class OrderToReturnDto
    {
        public Guid Id { get; set; }

        public string userEmail { get; set; } = null!;

        public DateTimeOffset OrderDate { get; set; }

        public AddressDto  Address { get; set; } = null!;

        public string DeliveryMethod { get; set; } = null!;//esm el dm

        public string orderState { get; set; } = null!;

        public ICollection<OrderItemDto> Items { get; set; } = [];

        public decimal subtotal { get; set; }   

        public decimal total { get; set; }

    }
}
