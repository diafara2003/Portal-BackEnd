using Code.Repository.Session.Implementation;
using Code.Repository.Session.Operations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Threading.Tasks;

namespace API.Attributes
{
    [AttributeUsage(validOn: AttributeTargets.Class | AttributeTargets.Method)]
    public class ApiKeyAttribute : Attribute, IAsyncActionFilter
    {
        
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.HttpContext.Request.Headers.TryGetValue(IconstructoraRepository.APIKEYNAME, out var extractedApiKey))
            {
                context.Result = new ContentResult()
                {
                    StatusCode = 401,
                    Content = "Api Key was not provided"
                };
                return;
            }



            var apiKey = new ApiKeyBL().GetContructoraKey(extractedApiKey);

            if (apiKey == null || apiKey.ConstId == 0)
            {
                context.Result = new ContentResult()
                {
                    StatusCode = 401,
                    Content = "Api Key is not valid"
                };
                return;
            }

            

            await next();
        }
    }
}
