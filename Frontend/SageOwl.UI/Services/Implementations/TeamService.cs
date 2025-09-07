using SageOwl.UI.Models;
using SageOwl.UI.Services.Interfaces;
using SageOwl.UI.ViewModels.Teams;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Text.Json;

namespace SageOwl.UI.Services.Implementations;

public class TeamService : ITeamService
{
    private readonly HttpClient _httpClient;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public TeamService(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)
    {
        _httpClient = httpClientFactory.CreateClient("Backend");
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<bool> CreateTeam(CreateTeamViewModel newTeam)
    {
        var token = _httpContextAccessor.HttpContext?.Request.Cookies["AccessToken"];
        if (string.IsNullOrEmpty(token))
            return false; 

        var json = JsonSerializer.Serialize(newTeam);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        _httpClient.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        var response = await _httpClient.PostAsync("team", content);

        return response.IsSuccessStatusCode;
    }

    public async Task<Team> GetTeamById(Guid teamId)
    {
        var token = _httpContextAccessor.HttpContext?.Request.Cookies["AccessToken"];

        if (string.IsNullOrEmpty(token))
            throw new Exception("Token was not found");

        var handler = new JwtSecurityTokenHandler();
        var jwt = handler.ReadJwtToken(token);

        var request = new HttpRequestMessage(HttpMethod.Get, $"team/id/{teamId}");
        request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        var response = await _httpClient.SendAsync(request);

        if (!response.IsSuccessStatusCode)
            return null;

        var content = await response.Content.ReadAsStringAsync();

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        var team = JsonSerializer.Deserialize<Team>(content, options);

        Console.WriteLine(team.Name);
        return team;

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
