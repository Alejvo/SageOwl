using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.IdentityModel.Tokens.Jwt;


namespace SageOwl.UI.Attributes;

public class AuthorizeTokenAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var httpContext = context.HttpContext;

        var accessToken = httpContext.Items["AccessToken"] as string
                          ?? httpContext.Request.Cookies["AccessToken"];
        var refreshToken = httpContext.Items["RefreshToken"] as string
                           ?? httpContext.Request.Cookies["RefreshToken"];

        if (string.IsNullOrEmpty(accessToken) && string.IsNullOrEmpty(refreshToken))
        {
            RedirectToLogin(context);
            return;
        }

        if (!string.IsNullOrEmpty(accessToken))
        {
            if (IsTokenValid(accessToken))
            {
                base.OnActionExecuting(context);
                return;
            }

            RedirectToLogin(context);
            return;
        }

        base.OnActionExecuting(context);
    }

    private static bool IsTokenValid(string token)
    {
        try
        {
            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(token);
            return jwt.ValidTo > DateTime.UtcNow;
        }
        catch
        {
            return false;
        }
    }

    private static void RedirectToLogin(ActionExecutingContext context)
    {
        context.Result = new RedirectToRouteResult(new RouteValueDictionary
        {
            { "Controller", "Account" },
            { "Action", "Login" }
        });
    }
}
