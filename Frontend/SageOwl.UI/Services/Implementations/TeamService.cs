using SageOwl.UI.Models;
using SageOwl.UI.Models.Teams;
using SageOwl.UI.Services.Interfaces;
using SageOwl.UI.ViewModels.Teams;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Text;
using System.Text.Json;

namespace SageOwl.UI.Services.Implementations;

public class TeamService : ITeamService
{
    private readonly HttpClient _httpClient;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IAuthService _authService;

    public TeamService(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor, IAuthService authService)
    {
        _httpClient = httpClientFactory.CreateClient("Backend");
        _httpContextAccessor = httpContextAccessor;
        _authService = authService;
    }

    public async Task<HttpStatusCode> CreateTeam(CreateTeamViewModel newTeam)
    {
        var json = JsonSerializer.Serialize(newTeam);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync("team", content);

        return response.StatusCode;
    }

    public async Task<HttpStatusCode> DeleteTeam(Guid teamId)
    {
        var response = await _httpClient.DeleteAsync($"team/{teamId}");

        return response.StatusCode;
    }

    public async Task<Team> GetTeamById(Guid teamId)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, $"team/id/{teamId}");

        var response = await _httpClient.SendAsync(request);

        if (!response.IsSuccessStatusCode)
            return null;

        var content = await response.Content.ReadAsStringAsync();

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        var team = JsonSerializer.Deserialize<Team>(content, options);

        return team;

    }

    public async Task<List<Team>> GetTeamsByUser()
    {

        var token = await _authService.GetAccessTokenAsync();

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

    public async Task<HttpStatusCode> UpdateTeam(UpdateTeamDto updateTeam)
    {
        var json = JsonSerializer.Serialize(updateTeam);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PutAsync("team", content);

        return response.StatusCode;
    }
}
