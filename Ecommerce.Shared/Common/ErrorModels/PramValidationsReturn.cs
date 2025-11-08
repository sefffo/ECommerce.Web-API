using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Shared.Common.ErrorModels
{
    public class PramValidationsReturn
    {
        public int statusCode { set; get; } = (int)HttpStatusCode.BadRequest;
        public string Message { get; set; } = "Validation Error";

        public IEnumerable<PramValidationError> Errors { get; set; }
    }
}
