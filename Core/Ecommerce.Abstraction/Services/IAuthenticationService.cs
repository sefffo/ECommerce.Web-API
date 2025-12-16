using Ecommerce.Shared.DTOs.IdentityDto_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Abstraction.Services
{
    public interface IAuthenticationService
    {
        Task<UserDto> LoginAsync(LoginDto dto);
        Task<UserDto> RegisterAsync(RegisterDto dto);

        Task<bool> CheckEmailAsync(string Email);
        Task<AddressDto> GetCurrentUserAddressAsync(string Email);
        Task<AddressDto> UpdateCurrentUserAddressAsync(string email,AddressDto dto);
        Task<UserDto> GetCurrentUserAsync(string email);

        //adding OAuth
        Task<UserDto> GetOrCreateGoogleUserAsync(string email, string displayName, string googleId);
    }
}
