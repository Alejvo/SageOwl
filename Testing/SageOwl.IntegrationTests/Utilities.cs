using Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Headers;

namespace SageOwl.IntegrationTests;

internal static class Utilities
{
    internal static async Task AuthenticateAsync(
        HttpClient client,
        IServiceProvider services)
    {
        using var scope =  services.CreateScope();

        var authService = scope.ServiceProvider.GetRequiredService<IAuthService>();

        var loginResult = await authService.LoginAsync("user@test.com", "123456");

        client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", loginResult.Value.AccessToken);
    }
}
