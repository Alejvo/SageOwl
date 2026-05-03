using SageOwl.UI.Services.Interfaces;

namespace SageOwl.UI.Middleware;

public class TokenRefreshMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    public async Task InvokeAsync(
        HttpContext context,
        IAuthService authService)
    {
        var accessToken = context.Request.Cookies["AccessToken"];
        var refreshToken = context.Request.Cookies["RefreshToken"];

        // No Session
        if (string.IsNullOrWhiteSpace(accessToken) &&
            string.IsNullOrWhiteSpace(refreshToken))
        {
            await _next(context);
            return;
        }

        var needsRefresh =
            string.IsNullOrWhiteSpace(accessToken) ||
            authService.IsTokenExpired(accessToken);

        // AccessToken Expired
        if (needsRefresh)
        {
            if (string.IsNullOrWhiteSpace(refreshToken))
            {
                await ClearCookies(context);

                context.Response.Redirect("/Account/Login");
                return;
            }
            try
            {
                var tokens = await authService.RefreshToken(refreshToken);

                if (tokens == null)
                {
                    await ClearCookies(context);

                    context.Response.Redirect("/Account/Login");
                    return;
                }

                accessToken = tokens.AccessToken;
                refreshToken = tokens.RefreshToken;

                SaveTokens(context, accessToken, refreshToken);

            }
            catch
            {
                await ClearCookies(context);

                context.Response.Redirect("/Account/Login");
                return;
            }
        }

        if (!string.IsNullOrWhiteSpace(accessToken))
        {
            context.Items["AccessToken"] = accessToken;
        }

        await _next(context);
    }

    private static void SaveTokens(
        HttpContext context,
        string accessToken,
        string refreshToken)
    {
        context.Response.Cookies.Append(
            "AccessToken",
            accessToken,
            new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTimeOffset.UtcNow.AddMinutes(15)
            });

        context.Response.Cookies.Append(
            "RefreshToken",
            refreshToken,
            new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTimeOffset.UtcNow.AddDays(7)

            });
    }

    private static Task ClearCookies(HttpContext context)
    {
        context.Response.Cookies.Delete("AccessToken");
        context.Response.Cookies.Delete("RefreshToken");

        return Task.CompletedTask;
    }
}
