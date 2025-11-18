using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Domain.Models.Orders
{
    public class OrderItem:BaseEntity<int>
    {

        //each item needs an id and it will be generated throw the EF Auto
        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public ItemTobeOrderd Product {  get; set; }
    }
}
