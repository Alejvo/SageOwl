using SageOwl.UI.Models.Tokens;
using SageOwl.UI.Services.Interfaces;
using SageOwl.UI.ViewModels.UI;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Text.Json;

namespace SageOwl.UI.Services.Implementations;

public class AuthService : IAuthService
{
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly HttpClient _httpClient;

    public AuthService(
        IHttpContextAccessor contextAccessor,
        IHttpClientFactory httpClientFactory)
    {
        _contextAccessor = contextAccessor;
        _httpClient = httpClientFactory.CreateClient("Auth");
    }

    public async Task<string?> GetAccessTokenAsync()
    {
        var context = _contextAccessor.HttpContext
                      ?? throw new InvalidOperationException("The HTTP Context couldn't be accessed");

        var accessToken = context.Request.Cookies["AccessToken"];
        var refreshToken = context.Request.Cookies["RefreshToken"];

        if (string.IsNullOrEmpty(accessToken))
        {
            if (string.IsNullOrEmpty(refreshToken))
                return null;

            var newToken = await RefreshToken(refreshToken);
            return newToken.AccessToken;
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

            var newToken = await RefreshToken(refreshToken);
            return newToken.AccessToken;
        }

        return accessToken;
    }

    public async Task<Token> RefreshToken(string refreshToken)
    {
        var body = JsonSerializer.Serialize(new { RefreshToken = refreshToken });
        var content = new StringContent(body, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync($"auth/refresh", content);
        if (!response.IsSuccessStatusCode)
            return null;

        var responseString = await response.Content.ReadAsStringAsync();
        var tokens = JsonSerializer.Deserialize<Token>(responseString, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        if (tokens == null || string.IsNullOrEmpty(tokens.AccessToken))
            return null;

       _contextAccessor.HttpContext.Response.Cookies.Append("AccessToken", tokens.AccessToken, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
            Expires = DateTimeOffset.UtcNow.AddMinutes(15)
        });

        _contextAccessor.HttpContext.Response.Cookies.Append("RefreshToken", tokens.RefreshToken, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
            Expires = DateTimeOffset.UtcNow.AddDays(7)
        });

        return tokens;
    }

    public bool IsTokenExpired(string? token)
    {
        if (string.IsNullOrEmpty(token)) return true;
        try
        {
            var jwt = new JwtSecurityTokenHandler().ReadJwtToken(token);
            return jwt.ValidTo <= DateTime.UtcNow;
        }
        catch
        {
            return true;
        }
    }

    public async Task<Token> Login(LoginViewModel viewModel)
    {
        var response = await _httpClient.PostAsJsonAsync("auth/login", viewModel);

        if (!response.IsSuccessStatusCode)
            return null;

        var loginResponse = await response.Content.ReadFromJsonAsync<Token>();

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
