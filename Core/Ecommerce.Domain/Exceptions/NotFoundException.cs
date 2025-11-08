using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Domain.Exceptions
{
    public  class NotFoundException(string Message) : Exception(Message)
    {

    }
}
