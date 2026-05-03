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

    public string? GetAccessToken()
    {
        var context = _contextAccessor.HttpContext;

        if (context == null)
            return null;

        if(context.Items.TryGetValue("AccessToken", out var cachedToken))
        {
            return cachedToken?.ToString();
        }
        return context.Request.Cookies["AccessToken"];

    }

    public async Task<Token?> RefreshToken(string refreshToken)
    {
        var context = _contextAccessor.HttpContext;

        if (context == null)
            return null;

        if(string.IsNullOrEmpty(refreshToken))
            return null;


        var body = JsonSerializer.Serialize( new 
        { 
            RefreshToken = refreshToken 
        });

        var content = new StringContent(
            body, 
            Encoding.UTF8, 
            "application/json");

        HttpResponseMessage response;
        try
        {
            response = await _httpClient.PostAsync($"auth/refresh", content);
        }
        catch
        {
            return null;
        }

        if (!response.IsSuccessStatusCode)
        {
            DeleteCookies();
            return null;
        }

        var responseString = await response.Content.ReadAsStringAsync();
        var tokens = JsonSerializer.Deserialize<Token>(responseString, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        if (tokens == null || 
            string.IsNullOrEmpty(tokens.AccessToken) ||
            string.IsNullOrEmpty(tokens.RefreshToken))
        {
            DeleteCookies();
            return null;
        }

        AppendCookies(tokens);

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

    public async Task<Token?> Login(LoginViewModel viewModel)
    {
        var response = await _httpClient.PostAsJsonAsync("auth/login", viewModel);

        if (!response.IsSuccessStatusCode)
            return null;

        var loginResponse = await response.Content.ReadFromJsonAsync<Token>();

        if (loginResponse == null)
            return null;

        AppendCookies(loginResponse);
        return loginResponse;
    }

    public void Logout()
    {
        DeleteCookies();
    }
    private void AppendCookies(Token tokens)
    {
        var context = _contextAccessor.HttpContext;

        if (context == null)
            return;

        context.Response.Cookies.Append(
            "AccessToken",
            tokens.AccessToken,
            new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Lax,
                Expires = DateTimeOffset.UtcNow.AddMinutes(15),
                Path = "/"
            });

        context.Response.Cookies.Append(
            "RefreshToken",
            tokens.RefreshToken,
            new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Lax,
                Expires = DateTimeOffset.UtcNow.AddDays(7),
                Path = "/"
            });
    }

    private void DeleteCookies()
    {
        var context = _contextAccessor.HttpContext;

        if (context == null)
            return;

        context.Response.Cookies.Delete(
            "AccessToken",
            new CookieOptions
            {
                Path = "/"
            });

        context.Response.Cookies.Delete(
            "RefreshToken",
            new CookieOptions
            {
                Path = "/"
            });
    }
}
