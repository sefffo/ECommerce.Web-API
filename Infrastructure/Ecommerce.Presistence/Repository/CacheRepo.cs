using Ecommerce.Domain.Models.Contracts.Repository;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Presistence.Repository
{

    //we use teh connection multiplixer to just use the redis memory data base 
    public class CacheRepo (IConnectionMultiplexer connectionMultiplexer) : ICacheRepo
    {

        private IDatabase _database = connectionMultiplexer.GetDatabase();//now we have the server 

        public async Task<string?> GetAsync(string cacheKey)
        {
            var CacheValue = await _database.StringGetAsync(cacheKey); // To get the data from the memory 
            return CacheValue.IsNullOrEmpty? null : CacheValue .ToString();
        }

        public async Task setAsync(string CacheKey, string CacheValue, TimeSpan TimeToLive)
        {
            await _database.StringSetAsync(CacheKey, CacheValue, TimeToLive);
        }
    }
}
