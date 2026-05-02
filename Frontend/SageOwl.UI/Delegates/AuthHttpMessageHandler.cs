using SageOwl.UI.Services.Interfaces;
using System.Net.Http.Headers;

namespace SageOwl.UI.Delegates;

public class AuthHttpMessageHandler : DelegatingHandler
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IAuthService _authService;

    public AuthHttpMessageHandler(
        IHttpContextAccessor httpContextAccessor, 
        IAuthService authService)
    {
        _httpContextAccessor = httpContextAccessor;
        _authService = authService;
    }

    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request, 
        CancellationToken cancellationToken)
    {
        var context = _httpContextAccessor.HttpContext!;

        if (context == null)
            return await base.SendAsync(request, cancellationToken);

        var accessToken = context.Request.Cookies["AccessToken"];
        var refreshToken = context.Request.Cookies["RefreshToken"];

        if (string.IsNullOrWhiteSpace(accessToken))
        {
            return await base.SendAsync(request, cancellationToken);
        }

        if (_authService.IsTokenExpired(accessToken))
        {
            if (string.IsNullOrWhiteSpace(refreshToken))
            {
                return await base.SendAsync(request, cancellationToken);
            }

            var tokens = await _authService.RefreshToken(refreshToken);

            if (tokens == null)
            {
                return await base.SendAsync(request, cancellationToken);
            }

            accessToken = tokens.AccessToken;
        }

        request.Headers.Authorization =
            new AuthenticationHeaderValue("Bearer", accessToken);

        return await base.SendAsync(request, cancellationToken);
    }
}
