using SageOwl.UI.Models.Users;
using SageOwl.UI.Services.Interfaces;
using SageOwl.UI.ViewModels.Users;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Text;
using System.Text.Json;

namespace SageOwl.UI.Services.Implementations;

public class UserService : IUserService
{
    private readonly HttpClient _httpClient;
    private readonly IAuthService _authService;
    private readonly IHttpContextAccessor _httpContextAccessor;


    public UserService(
        IHttpClientFactory httpClientFactory, 
        IHttpContextAccessor httpContextAccessor,
        IAuthService authService)
    {
        _httpClient = httpClientFactory.CreateClient("Backend");
        _httpContextAccessor = httpContextAccessor;
        _authService = authService;
    }

    public async Task<HttpStatusCode> Create(RegisterViewModel data)
    {
        var json = JsonSerializer.Serialize(data);
        var content = new StringContent(json,Encoding.UTF8,"application/json");
        var response = await _httpClient.PostAsync("users",content);
        return response.StatusCode;
    }

    public async Task<HttpStatusCode> DeleteUser(Guid userId)
    {
        var response = await _httpClient.DeleteAsync($"users/{userId}");

        return response.StatusCode;
    }

    public async Task<User?> GetUserFromToken(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        var jwtToken = handler.ReadJwtToken(token);

        var userIdClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "sub");
        var userId = userIdClaim?.Value;

        if (string.IsNullOrEmpty(userId))
            return null;

        var request = new HttpRequestMessage(HttpMethod.Get, $"users/id/{userId}");
        request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        var response = await _httpClient.SendAsync(request);

        if (!response.IsSuccessStatusCode)
            return null;

        var content = await response.Content.ReadAsStringAsync();

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        var user = JsonSerializer.Deserialize<User>(content, options);
        return user;
    }

    public async Task<List<User>> GetUsers(int Page, int PageSize, string? SearchTerm, string? SortColumn, string? SortOrder)
    {
        var token = _httpContextAccessor.HttpContext?.Request.Cookies["AccessToken"];

        if (string.IsNullOrEmpty(token))
            throw new Exception("Token was not found");

        var handler = new JwtSecurityTokenHandler();
        var jwt = handler.ReadJwtToken(token);

        var userId = jwt.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;

        if (string.IsNullOrEmpty(userId))
            throw new Exception("Claim 'sub' was not found");

        var request = new HttpRequestMessage(HttpMethod.Get, $"users?searchTerm={SearchTerm}&sortColumn={SortColumn}&sortOrder={SortOrder}&page={Page}&pageSize={PageSize}");
        request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        var response = await _httpClient.SendAsync(request);

        if (!response.IsSuccessStatusCode)
            return null;

        var content = await response.Content.ReadAsStringAsync();

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        var users = JsonSerializer.Deserialize<List<User>>(content, options);
        return users;
    }

    public async Task<HttpStatusCode> Update(UpdateUserViewModel user)
    {
        var token = await _authService.GetAccessTokenAsync();

        var json = JsonSerializer.Serialize(user);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        _httpClient.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        var response = await _httpClient.PutAsync("user", content);

        return response.StatusCode;
    }
}
