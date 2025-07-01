using Microsoft.AspNetCore.Identity.Data;
using SageOwl.UI.Models;
using SageOwl.UI.ViewModel;
using System.Text.Json;
using System.Text;

namespace SageOwl.UI.Services;

public class AccountService : IAccountService
{
    private readonly HttpClient _httpClient;
    private readonly IHttpContextAccessor _contextAccessor;

    public AccountService(IHttpClientFactory httpClientFactory, IHttpContextAccessor contextAccessor)
    {
        _httpClient = httpClientFactory.CreateClient("Backend");
        _contextAccessor = contextAccessor;
    }
    public async Task<LoginResponse?> Login(LoginViewModel viewModel)
    {
        var response = await _httpClient.PostAsJsonAsync("auth/login", viewModel);

        if (!response.IsSuccessStatusCode)
            return null;

        var loginResponse = await response.Content.ReadFromJsonAsync<LoginResponse>();
        Console.WriteLine($"LoginResponse: {loginResponse}");

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

        return loginResponse;
    }
}
