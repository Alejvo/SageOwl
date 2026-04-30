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

        if (string.IsNullOrEmpty(accessToken))
        {
            if (string.IsNullOrEmpty(refreshToken))
            {
                RedirectToLogin(context);
                return;
            }

            base.OnActionExecuting(context);
            return;
        }

        if (IsTokenExpired(accessToken))
        {
            if (string.IsNullOrEmpty(refreshToken))
            {
                RedirectToLogin(context);
                return;
            }

            base.OnActionExecuting(context);
            return;
        }

        base.OnActionExecuting(context);
    }

    private static bool IsTokenExpired(string token)
    {
        try
        {
            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(token);

            return jwt.ValidTo <= DateTime.UtcNow;
        }
        catch
        {
            return true;
        }
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
