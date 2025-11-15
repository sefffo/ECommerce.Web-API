
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Shared.DTOs.IdentityDto_s
{
    public class AddressDto
    {
        public string Street { set; get; } = null!;
        public string Country { set; get; } = null!;
        public string City { set; get; } = null!;
        public string FirstName { set; get; } = null!;
        public string LastName { set; get; }=null!;

    }
}
