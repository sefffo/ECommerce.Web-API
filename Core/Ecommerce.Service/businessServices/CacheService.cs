using Ecommerce.Abstraction.Services;
using Ecommerce.Domain.Models.Contracts.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Ecommerce.Service.businessServices
{
    public class CacheService(ICacheRepo cacheRepo) : ICacheService
    {
        public async Task<string?> GetAsync(string CacheKey)
        {
            var ChacheValue = await cacheRepo.GetAsync(CacheKey);
            return ChacheValue;
        }

        public async Task SetAsync(string CacheKey, object cacheValue, TimeSpan TimeToLive)
        {
            var value = JsonSerializer.Serialize(cacheValue); //el value tb2a json  object - > json

            await cacheRepo.setAsync(CacheKey, value, TimeToLive);

        }
    }
}
