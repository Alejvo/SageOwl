using SageOwl.UI.Models;
using SageOwl.UI.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Text;
using System.Text.Json;

namespace SageOwl.UI.Services.Implementations;

public class QualificationService : IQualificationService
{
    private readonly HttpClient _httpClient;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public QualificationService(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)
    {
        _httpClient = httpClientFactory.CreateClient("Backend");
        _httpContextAccessor = httpContextAccessor;
    }
    public async Task<List<Qualification>> GetGetQualificationsByUserId(Guid userId)
    {
        var token = _httpContextAccessor.HttpContext?.Request.Cookies["AccessToken"];

        if (string.IsNullOrEmpty(token))
            throw new Exception("Token was not found");

        var handler = new JwtSecurityTokenHandler();
        var jwt = handler.ReadJwtToken(token);

        var request = new HttpRequestMessage(HttpMethod.Get, $"qualifications/userId/{userId}");
        request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        var response = await _httpClient.SendAsync(request);

        if (!response.IsSuccessStatusCode)
            return null;

        var content = await response.Content.ReadAsStringAsync();

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        var qualifications = JsonSerializer.Deserialize<List<Qualification>>(content, options);

        return qualifications;
    }

    public async Task<List<Qualification>> GetQualificationsByTeamId(Guid teamId)
    {
        var token = _httpContextAccessor.HttpContext?.Request.Cookies["AccessToken"];

        if (string.IsNullOrEmpty(token))
            throw new Exception("Token was not found");

        var handler = new JwtSecurityTokenHandler();
        var jwt = handler.ReadJwtToken(token);

        var request = new HttpRequestMessage(HttpMethod.Get, $"qualifications/teamId/{teamId}");
        request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        var response = await _httpClient.SendAsync(request);

        if (!response.IsSuccessStatusCode)
            return null;

        var content = await response.Content.ReadAsStringAsync();

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        var qualifications = JsonSerializer.Deserialize<List<Qualification>>(content, options);

        return qualifications;
    }

    public async Task<HttpStatusCode> SaveQualifications(Qualification qualification)
    {
        var token = _httpContextAccessor.HttpContext?.Request.Cookies["AccessToken"];

        var json = JsonSerializer.Serialize(qualification);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        _httpClient.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        var response = await _httpClient.PostAsync("team", content);

        return response.StatusCode;
    }
}
