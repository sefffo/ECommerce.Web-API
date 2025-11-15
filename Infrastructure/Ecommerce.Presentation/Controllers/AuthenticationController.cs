using Ecommerce.Abstraction.Services;
using Ecommerce.Shared.DTOs.IdentityDto_s;
using Microsoft.AspNetCore.Identity;
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
    [Route("api/[Controller]")]
    public class AuthenticationController(IServiceManger manger):ControllerBase
    {
        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto dto)
        {
            var User = await manger.AuthenticationService.LoginAsync(dto);
            return Ok(User);
        }
        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto dto)
        {
            var User = await manger.AuthenticationService.RegisterAsync(dto);
            return Ok(User);
        }

        [HttpGet("CheckEmail")]
        public async Task<ActionResult<bool>> CheckEmail(string email)
        {
            var user = await manger.AuthenticationService.CheckEmailAsync(email);
            return Ok(user);
        }
        [HttpGet("CurrentUser")]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await manger.AuthenticationService.GetCurrentUserAsync(email);
            return Ok(user);
        }
        [HttpGet("Address")]
        public async Task<ActionResult<UserDto>> GetCurrentUserAddress()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var address = await manger.AuthenticationService.GetCurrentUserAddressAsync(email);
            return Ok(address);
        }
        [HttpPut("Address")]
        public async Task<ActionResult<AddressDto>> UpdateCurrentUserAddress(AddressDto dto)
        {
            var Email = User.FindFirstValue(ClaimTypes.Email);
            var updatedAddress = await manger.AuthenticationService.UpdateCurrentUserAddressAsync(Email, dto);
            return Ok(updatedAddress);

        }
    }
}
