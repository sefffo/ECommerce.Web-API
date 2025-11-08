using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Domain.Models.Cart
{
    public class UserCart
    {
        //we dont store them in the data base only after the confirmation of the order
        //and also to lower the number of requests of the db 
        //so the Cart is a Temp Memory on the server 
        //so we ill use the in Memory DataBase key value Pair 
        public string Id { get; set; }
        public ICollection<CartItem> Items { get; set; }



    }
}
