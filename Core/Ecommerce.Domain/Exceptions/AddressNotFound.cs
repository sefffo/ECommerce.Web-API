using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Domain.Exceptions
{
    public class AddressNotFound(string name) :NotFoundException($"{name} Has No Address")
    {
    }
}
