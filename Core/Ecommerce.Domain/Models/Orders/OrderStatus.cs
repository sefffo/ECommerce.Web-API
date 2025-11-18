using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Domain.Models.Orders
{
    public enum OrderStatus
    {
        pending = 0,
        Shipped = 1,
        Deliverd = 2,
        Cancelled = 3,
    }
}
