using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.IdentityModel.Tokens.Jwt;


namespace SageOwl.UI.Attributes;

public class AuthorizeTokenAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var httpContext = context.HttpContext;

        var accessToken = httpContext.Request.Cookies["AccessToken"];

        var refreshToken = httpContext.Request.Cookies["RefreshToken"];

        if (string.IsNullOrWhiteSpace(accessToken) &&
            string.IsNullOrWhiteSpace(refreshToken))
        {
            RedirectToLogin(context);
            return;
        }

        base.OnActionExecuting(context);
    }

    private static void RedirectToLogin(ActionExecutingContext context)
    {
        context.Result = new RedirectToRouteResult(
            new RouteValueDictionary
            {
                { "controller", "Account" },
                { "action", "Login" }
            });
    }
}
