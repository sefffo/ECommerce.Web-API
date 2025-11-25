using Ecommerce.Domain.Models.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Service.Specifications
{
    public class OrderIntentCheck:BaseSpecifications<Order, Guid>
    {

        public OrderIntentCheck(string IntentId) : base(o => o.PaymentIntentId == IntentId)
        {

        }
    }
}
