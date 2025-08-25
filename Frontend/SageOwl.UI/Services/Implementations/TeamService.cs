using SageOwl.UI.Models;
using SageOwl.UI.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Text.Json;

namespace SageOwl.UI.Services.Implementations;

public class TeamService : ITeamService
{
    private readonly HttpClient _httpClient;
    public TeamService(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("Backend");
    }

    public async Task<List<Team>> GetTeamsByUserToken(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        var jwtToken = handler.ReadJwtToken(token);

        var userIdClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "sub");
        var userId = userIdClaim?.Value;

        if (string.IsNullOrEmpty(userId))
            return null;

        var request = new HttpRequestMessage(HttpMethod.Get, $"team/userid/{userId}");
        request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        var response = await _httpClient.SendAsync(request);

        if (!response.IsSuccessStatusCode)
            return null;

        var content = await response.Content.ReadAsStringAsync();

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        var teams = JsonSerializer.Deserialize<List<Team>>(content, options);
        return teams;
    }
}
