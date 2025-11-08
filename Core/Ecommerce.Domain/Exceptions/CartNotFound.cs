using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Domain.Exceptions
{
    public class CartNotFound(string Id) : NotFoundException($"Cart with id {Id} is not Found")
    {
    }
}
