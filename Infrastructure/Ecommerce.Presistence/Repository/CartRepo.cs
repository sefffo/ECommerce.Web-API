using Ecommerce.Domain.Models.Cart;
using Ecommerce.Domain.Models.Contracts.RedisInMemoryRepository;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Ecommerce.Presistence.Repository
{
    //we need to use the connection with the redis memory data base insted the normal that we pass the context  
    public class CartRepo(IConnectionMultiplexer connection) : ICartRepo
    {
        private readonly IDatabase dataBase = connection.GetDatabase();
        public async Task<UserCart?> GetCartAsync(string key)
        {
            var Cart = await dataBase.StringGetAsync(key); //return redis Value so we need to change it 
            if (Cart.IsNullOrEmpty) return null;
            else return JsonSerializer.Deserialize<UserCart>(Cart);
        }
        public async Task<UserCart?> CreateUpdateCartAsync(UserCart cart, TimeSpan? timeSpan = null)
        {
            var JsonCart = JsonSerializer.Serialize(cart);
                                                                    //key  and value 
            var isCreatedOrUpdated = await dataBase.StringSetAsync(cart.Id, JsonCart, timeSpan ?? TimeSpan.FromHours(5));

            if (isCreatedOrUpdated)
            {
                return await GetCartAsync(cart.Id);
            }
            else
                return null;
        }
        public async Task<bool?> DeleteCartAsync(string Key)
        {
            return await dataBase.KeyDeleteAsync(Key);
        }

    }
}
