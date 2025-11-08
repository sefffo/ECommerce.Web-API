using Ecommerce.Domain.Models.Cart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Domain.Models.Contracts.RedisInMemoryRepository
{
    public interface ICartRepo
    {
        //CURD Operation in Redis


        Task<UserCart?>GetCartAsync(string key);
        Task<UserCart?> CreateUpdateCartAsync( UserCart cart , TimeSpan? timeSpan = null);
        Task<bool?> DeleteCartAsync(string Key);



    }
}
