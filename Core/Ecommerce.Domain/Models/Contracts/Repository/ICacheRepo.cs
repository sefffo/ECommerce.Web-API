using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Domain.Models.Contracts.Repository
{
    public interface ICacheRepo
    {
        //get data
        Task<string?> GetAsync(string cacheKey);

        //set data
        Task setAsync(string CacheKey ,string CacheValue,TimeSpan TimeToLive);
    }
}
