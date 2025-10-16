using Microsoft.AspNetCore.Identity.Data;
using SageOwl.UI.Models;
using SageOwl.UI.Services.Interfaces;
using SageOwl.UI.ViewModels;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace SageOwl.UI.Services.Implementations;

public class AccountService : IAccountService
{
    private readonly HttpClient _httpClient;
    private readonly IHttpContextAccessor _contextAccessor;

    public AccountService(IHttpClientFactory httpClientFactory, IHttpContextAccessor contextAccessor)
    {
        _httpClient = httpClientFactory.CreateClient("Backend");
        _contextAccessor = contextAccessor;
    }


    public async Task<string?> GetValidAccessTokenAsync()
    {
        var context = _contextAccessor.HttpContext
                      ?? throw new InvalidOperationException("No se pudo acceder al contexto HTTP.");

        var accessToken = context.Request.Cookies["AccessToken"];
        var refreshToken = context.Request.Cookies["RefreshToken"];

        if (string.IsNullOrEmpty(accessToken))
        {
            if (string.IsNullOrEmpty(refreshToken))
                return null;

            var newToken = await TryRefreshTokenAsync(context, refreshToken);
            return newToken;
        }

        var handler = new JwtSecurityTokenHandler();
        JwtSecurityToken jwt;
        try
        {
            jwt = handler.ReadJwtToken(accessToken);
        }
        catch
        {
            return null;
        }

        if (jwt.ValidTo <= DateTime.UtcNow)
        {
            if (string.IsNullOrEmpty(refreshToken))
                return null;

            var newToken = await TryRefreshTokenAsync(context, refreshToken);
            return newToken;
        }

        return accessToken;
    }
    private async Task<string?> TryRefreshTokenAsync(HttpContext context, string refreshToken)
    {
        var body = JsonSerializer.Serialize(new { RefreshToken = refreshToken });
        var content = new StringContent(body, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync($"auth/refresh", content);
        if (!response.IsSuccessStatusCode)
            return null;

        var responseString = await response.Content.ReadAsStringAsync();
        var tokens = JsonSerializer.Deserialize<TokenResponse>(responseString, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        if (tokens == null || string.IsNullOrEmpty(tokens.AccessToken))
            return null;

        context.Response.Cookies.Append("AccessToken", tokens.AccessToken, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
            Expires = DateTimeOffset.UtcNow.AddMinutes(15)
        });

        context.Response.Cookies.Append("RefreshToken", tokens.RefreshToken, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
            Expires = DateTimeOffset.UtcNow.AddDays(7)
        });

        return tokens.AccessToken;
    }

    private class TokenResponse
    {
        public string AccessToken { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
    }

    public async Task<LoginResponse?> Login(LoginViewModel viewModel)
    {
        var response = await _httpClient.PostAsJsonAsync("auth/login", viewModel);

        if (!response.IsSuccessStatusCode)
            return null;

        var loginResponse = await response.Content.ReadFromJsonAsync<LoginResponse>();

        if (loginResponse?.AccessToken is not null)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTimeOffset.UtcNow.AddMinutes(15)
            };

            _contextAccessor.HttpContext?.Response.Cookies.Append("AccessToken", loginResponse.AccessToken, cookieOptions);
        }

        if (loginResponse?.RefreshToken is not null)
        {
            var refreshTokenOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTimeOffset.UtcNow.AddDays(7)
            };

            _contextAccessor.HttpContext?.Response.Cookies.Append("RefreshToken", loginResponse.RefreshToken, refreshTokenOptions);
        }

        return loginResponse;
    }



}
