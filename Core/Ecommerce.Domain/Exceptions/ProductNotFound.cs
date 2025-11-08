using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Domain.Exceptions
{
    public sealed class ProductNotFound(int Id):NotFoundException($"product with id {Id} is not Found")
    {
    }
}
