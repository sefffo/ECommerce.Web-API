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

        //payment Gatway
        public string? ClientSecret { get; set; }

        public string? PaymentIntentId { get; set; }


        //3shan y3rf hydf3 ezay abl el checkout
        public int? DeliveryMethodId { get; set; }

        public decimal? ShippingPrice { get; set; }
    }
}
