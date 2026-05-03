using System.Net.Http.Headers;

namespace SageOwl.UI.Delegates;

public class AuthHttpMessageHandler(
    IHttpContextAccessor httpContextAccessor) : DelegatingHandler
{
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        var context = _httpContextAccessor.HttpContext;

        var accessToken =
            context?.Items["AccessToken"]?.ToString();

        if (!string.IsNullOrWhiteSpace(accessToken))
        {
            request.Headers.Authorization =
                new AuthenticationHeaderValue(
                    "Bearer",
                    accessToken);
        }

        return await base.SendAsync(
            request,
            cancellationToken);
    }
}