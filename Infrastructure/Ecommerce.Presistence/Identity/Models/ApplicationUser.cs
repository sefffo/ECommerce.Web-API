using Microsoft.AspNetCore.Identity;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Presistence.Identity.Models
{
    public class ApplicationUser:IdentityUser
    {
        public string DisplayName { get; set; } = null!;
        //relation

        //msh lazem el user yb2a 3ndo3nwan fe al awl 
        public Address? Address { get; set; }
    }
}
