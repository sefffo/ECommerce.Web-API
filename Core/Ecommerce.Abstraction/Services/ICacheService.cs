using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Abstraction.Services
{
    public interface ICacheService
    {
        Task<string?> GetAsync(string CacheKey);
        /// <summary>
        /// we pass the cacheValue as an object because we dont know the data to be seted yet 
        /// </summary>
        /// <param name="CacheKey"></param>
        /// <param name="cacheValue"></param>
        /// <param name="TimeToLive"></param>
        /// <returns></returns>
        Task SetAsync(string CacheKey , object cacheValue,TimeSpan TimeToLive);

    }
}
