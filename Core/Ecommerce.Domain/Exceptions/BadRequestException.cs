using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Domain.Exceptions
{
    public class BadRequestException(List<string>Errors):Exception("Validation Faild")
    {
        public List<string> Errors { get; set; } = Errors;
    }
}
