using Ecommerce.Shared.DTOs.OrderDto_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Abstraction.Services
{
    public interface IOrderService
    {
        //create order ==>OrderDto [adress - delivery method - CartId] / userEmail return OrderToReturn Dto [id , email , items , address , delivery methode , orderstate , total sub total ]
        Task<OrderToReturnDto> CreateOrderAsync(OrderDto orderDto,string Email );

        //Get All Delivery Methods ==> For the front End section 

        Task<IEnumerable<DeliveryMethodDto>> GetDeliveryMethodsAsync();


        //get all orders For Current User
        Task<IEnumerable<OrderToReturnDto>> GetAllOrdersAsync(string Email);


        //Get Specific Order For Current User
        Task<OrderToReturnDto> GetOrderById(Guid OrderId);


    }
}
