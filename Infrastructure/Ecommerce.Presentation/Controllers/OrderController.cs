using Ecommerce.Abstraction.Services;
using Ecommerce.Presentation.Attributes;
using Ecommerce.Shared.DTOs.OrderDto_s;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Presentation.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[Controller]")]
    public class OrderController(IServiceManger manger):ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<OrderToReturnDto>> CreateOrder(OrderDto orderDto)
        {

            var emailFromToken = User.FindFirstValue(ClaimTypes.Email);

            var Order = await manger.OrderService.CreateOrderAsync(orderDto, emailFromToken);

            return Ok(Order);
        }
        [HttpGet("DeliveryMethods")]
        public async Task<ActionResult <IEnumerable<DeliveryMethodDto>>> GetDeliveryMethods() 
        {
            var DMs = await manger.OrderService.GetDeliveryMethodsAsync();
            return Ok(DMs);
        }
        [HttpGet("AllOrders")]
        [Cache(4200)]
        public async Task<ActionResult<IEnumerable<OrderToReturnDto>>> GetAllOrders()
        {
            var Email = User.FindFirstValue (ClaimTypes.Email);
            var Orders = await manger.OrderService.GetAllOrdersAsync(Email);
            return Ok(Orders);
        }
        [HttpGet]
        [Cache(1000)]
        public async Task<ActionResult<OrderToReturnDto>> GetOrderById(Guid Orderid)
        {
            var Order = await manger.OrderService.GetOrderById(Orderid);
            return Ok(Order);
        }

    }
}
