using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;

namespace MVC_Webapp.Helpers
{
    public class APIKeyAuth : Attribute, IAsyncActionFilter
    {
        private const string ApiKeyUserID = "Authorization";

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.HttpContext.Request.Headers.TryGetValue(ApiKeyUserID, out var APIToken))
            {
                context.Result = new BadRequestResult();
                return;
            }
            else
            {
                var check = CheckAPIToken(APIToken);
                if (!check)
                {
                    context.Result = new UnauthorizedResult();
                    return;
                }
            }
            await next();
        }
        public static bool CheckAPIToken(string APIToken)
        {
            if (APIToken == "")
            {
                return false;
            }
            return true;
        }
    }
}

