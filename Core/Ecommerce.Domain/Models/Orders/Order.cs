using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Domain.Models.Orders
{
    public class Order:BaseEntity<Guid>
    {

        public Order() //3shan el EF y3rf yst8l 
        {
                
        }

        public Order(string userEmail, OrderAddress address, DeliveryMethod deliveryMethod, ICollection<OrderItem> items, decimal subTotal)
        {
            UserEmail = userEmail;
            Address = address;
            DeliveryMethod = deliveryMethod;
            Items = items;
            SubTotal = subTotal;
        }

        //the big entity which can collect all the order aspects in the folder 

        public string UserEmail { get; set; } = null!; //mn el Token 

        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;

        public OrderAddress Address { get; set; } = null!;


        //one to maney relation //y3ny el one byroh ll many 
        public DeliveryMethod DeliveryMethod { get; set; } = null!;
        [ForeignKey("DeliveryMethod")]
        public int DeliveryMethodeId { get; set; }     //el one el geh hena 


        public OrderStatus Status { get; set; }


        public ICollection<OrderItem> Items { get; set; } = [];
        

        public decimal SubTotal { get; set; } //el price of the item without the shipping and tax 

        //public decimal Total { get; set; } //El tax + Delivery on the sub total which they can be changble throw the time 
        public decimal Total () =>  SubTotal + DeliveryMethod.Price;
        
    }
}
