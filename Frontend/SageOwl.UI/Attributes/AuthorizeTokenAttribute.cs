using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace SageOwl.UI.Attributes;

public class AuthorizeTokenAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var token = context.HttpContext.Request.Cookies["AccessToken"];

        if (string.IsNullOrEmpty(token))
        {
            context.Result = new RedirectToRouteResult(
                new RouteValueDictionary
                {
                    { "Controller", "Account" },
                    { "Action", "Login" }
                });
        }
    }
}
