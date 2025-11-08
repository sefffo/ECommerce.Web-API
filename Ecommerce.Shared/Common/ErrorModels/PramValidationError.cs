using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Shared.Common.ErrorModels
{
    public class PramValidationError
    {
        public string Field { get; set; } = null!;
        public IEnumerable<string> Errors { get; set; }


    }
}
