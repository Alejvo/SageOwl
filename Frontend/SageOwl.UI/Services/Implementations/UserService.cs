using SageOwl.UI.Models;
using SageOwl.UI.Services.Interfaces;
using SageOwl.UI.ViewModel;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Text.Json;

namespace SageOwl.UI.Services.Implementations;

public class UserService : IUserService
{
    private readonly HttpClient _httpClient;

    public UserService(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("Backend");
    }

    public async Task<bool> Create(RegisterViewModel data)
    {
        var user = new
        {
            name=data.Name,
            data.Surname,
            data.Email,
            data.Password,
            data.Username,
            data.BirthDay
        };

        var json = JsonSerializer.Serialize(data);
        var content = new StringContent(json,Encoding.UTF8,"application/json");
        var response = await _httpClient.PostAsync("users",content);
        return response.IsSuccessStatusCode;
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
}
