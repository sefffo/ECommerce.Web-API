using Ecommerce.Abstraction.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;//the one resposible for the filter attribute 
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Presentation.Attributes
{
    public class CacheAttribute(int durationInSec=90):ActionFilterAttribute
    {
                                                    //context from the http           //the invoke for the next like the middlewares
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            //invoke when the cache is empty 

            //generate cachekey 
            string chaceKey = CreateCacheKey(context.HttpContext.Request);
            //search for the cache value //we create a custom service for our selves just to use for generating the behavior 
            ICacheService cacheService = context.HttpContext.RequestServices.GetRequiredService<ICacheService>();
            var CacheValue = await cacheService.GetAsync(chaceKey);//will call the repo of redis if there is data it will return if not it will move along 

            //return value if there is data and if null return diff data 
            if(CacheValue is not null)
            {
                context.Result = new ContentResult()
                {
                    Content = CacheValue,
                    ContentType = "application/json",
                    StatusCode = StatusCodes.Status200OK
                };
                return;
            } //if null i must invoke the next and then used it as a cachevallue and set a key for it 
            //invoke next code 
            var ExceutedContext = await next.Invoke();
            //set the value with cachekey 
            if (ExceutedContext.Result is ObjectResult resultJson) 
            {
                await cacheService.SetAsync(chaceKey, resultJson.Value, TimeSpan.FromSeconds(durationInSec));
            }
        }

                                // take the request type to get the path 
        private string CreateCacheKey(HttpRequest httpRequest) 
        {
            //{BaseUrl/api/products?TypeId=5&BrandId=6} ==> how the key gonna be saved 

            StringBuilder key = new StringBuilder();

            key.Append(httpRequest.Path + '?');

            foreach (var item in httpRequest.Query.OrderBy(q=>q.Key))
            {
                key.Append($"{item.Key}={item.Value}&");
            }
            return key.ToString();
        }
    }
}
