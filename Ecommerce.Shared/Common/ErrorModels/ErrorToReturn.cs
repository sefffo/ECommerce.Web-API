using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Shared.Common.ErrorModels
{
    public class ErrorToReturn
    {
        public int statusCode { set; get; }
        public string Message { get; set; } = null!;

        public List<string>? Errors { get; set; }
    }
}
