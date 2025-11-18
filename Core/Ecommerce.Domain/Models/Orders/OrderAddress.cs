using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Domain.Models.Orders
{
    public class OrderAddress
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set;} = null!;
        public string Street { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Country { get; set; } = null!;
        //kol order lazem ykon leh address 
        //w kol address lazm ykon leh order 


    }
}
