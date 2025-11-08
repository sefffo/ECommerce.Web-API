using Ecommerce.Shared.DTOs.CartDto_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Abstraction.Services
{
    public interface ICartService
    {
        Task<CartDto> GetCartAsync(string key);
        Task<CartDto> CreateUpdateCartAsync(CartDto Cart);
        Task<bool> DeleteCartAsync(string key);
        
    }
}
