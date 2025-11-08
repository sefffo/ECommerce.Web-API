using Azure;
using Ecommerce.Domain.Exceptions;
using Ecommerce.Shared.Common.ErrorModels;
using Microsoft.AspNetCore.Http;

namespace ECommerce.Web.Custom_MiddleWares
{
    public class CustomExceptionMiddleWare
    {
        private readonly RequestDelegate next;

        public CustomExceptionMiddleWare(RequestDelegate Next)
        {
            next = Next;
        }
        public async Task Invoke(HttpContext context, ILogger<CustomExceptionMiddleWare> logger)
        {
            try
            {
                await next.Invoke(context);
                if (context.Response.StatusCode == StatusCodes.Status404NotFound)
                {

                    var Response = new ErrorToReturn()
                    {
                        statusCode = StatusCodes.Status404NotFound,
                        Message = $"this End Point {context.Request.Path} is Not Found"
                    };
                    await context.Response.WriteAsJsonAsync(Response);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                //header response
                //set Satus Code 
                context.Response.StatusCode = ex switch
                {
                    NotFoundException => StatusCodes.Status404NotFound,

                    _ => StatusCodes.Status500InternalServerError

                };
                //Set Content Type For response 

                context.Response.ContentType = "application/json"; //elrage3 json

                //responser body 

                var Response = new ErrorToReturn()
                {
                    statusCode = context.Response.StatusCode,
                    Message = ex.Message
                };

                //return as json 

                await context.Response.WriteAsJsonAsync(Response);

            }
        }
    }
}
