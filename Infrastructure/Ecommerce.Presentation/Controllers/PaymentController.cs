using Ecommerce.Abstraction.Services;
using Ecommerce.Shared.DTOs.CartDto_s;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Presentation.Controllers
{
    [ApiController]
  
    [Route("api/[Controller]")] //routing el endpoint
    public class PaymentController(IServiceManger manger):ControllerBase
    {
        [Authorize]
        [HttpPost("{CartId}")]
        public async Task<ActionResult<CartDto>> CreateOrUpdatePAymentIntent(string CartId) //come from token 
        {
            var Cart = await manger.PaymentService.CreateOrUpdatePaymentIntentAsync(CartId);
            return Ok(Cart);
        }
    }
}
