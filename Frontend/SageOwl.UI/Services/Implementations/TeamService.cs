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
    private readonly IAccountService _accountService;

    public TeamService(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor, IAccountService accountService)
    {
        _httpClient = httpClientFactory.CreateClient("Backend");
        _httpContextAccessor = httpContextAccessor;
        _accountService = accountService;
    }

    public async Task<bool> CreateTeam(CreateTeamViewModel newTeam)
    {
        var token = await _accountService.GetValidAccessTokenAsync();

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
        var token = await _accountService.GetValidAccessTokenAsync();

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

        return team;

    }

    public async Task<List<Team>> GetTeamsByUser()
    {

        var token = await _accountService.GetValidAccessTokenAsync();

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

    public async Task<bool> UpdateTeam(UpdateTeamDto updateTeam)
    {
        var token = await _accountService.GetValidAccessTokenAsync();
        if (string.IsNullOrEmpty(token))
            return false;

        var json = JsonSerializer.Serialize(updateTeam);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        _httpClient.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        var response = await _httpClient.PutAsync("team", content);

        return response.IsSuccessStatusCode;
    }
}
