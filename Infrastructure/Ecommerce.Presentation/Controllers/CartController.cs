using Ecommerce.Abstraction.Services;
using Ecommerce.Shared.DTOs.CartDto_s;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Presentation.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[Controller]")] //routing el endpoint
    public class CartController(IServiceManger manger) : ControllerBase
    {

        [HttpGet]
        public async Task<ActionResult<CartDto>> GetCart( string Key)
        {
            var Cart = await manger.CartService.GetCartAsync(Key);
            return Ok(Cart);
        }
        [HttpPost]
        public async Task<ActionResult<CartDto>> CraetUpdateCart(CartDto cart)
        {
            var CartTobeSaved = await manger.CartService.CreateUpdateCartAsync(cart);
            return Ok(CartTobeSaved);
        }
        [HttpDelete("{Key}")]
        public async Task<ActionResult<bool>> DeleteCart( string Key)
        {

            var data = await manger.CartService.DeleteCartAsync(Key);
            return Ok(data);
        }


    }
}
