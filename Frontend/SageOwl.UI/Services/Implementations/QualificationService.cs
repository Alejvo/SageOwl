using SageOwl.UI.Models.Qualifications;
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
    private readonly IAccountService _accountService;

    public QualificationService(
        IHttpClientFactory httpClientFactory, 
        IHttpContextAccessor httpContextAccessor,
        IAccountService accountService)
    {
        _httpClient = httpClientFactory.CreateClient("Backend");
        _httpContextAccessor = httpContextAccessor;
        _accountService = accountService;
    }
    public async Task<List<Qualification>> GetQualificationByUserId(Guid userId)
    {
        var token = _httpContextAccessor.HttpContext?.Request.Cookies["AccessToken"];

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

        var qualification = JsonSerializer.Deserialize<List<Qualification>>(content, options);

        return qualification;
    }

    public async Task<List<Qualification>> GetQualificationByTeamId(Guid teamId)
    {
        var token = await _accountService.GetValidAccessTokenAsync();

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

        var qualification = JsonSerializer.Deserialize<List<Qualification>>(content, options);

        return qualification;
    }

    public async Task<HttpStatusCode> SaveQualifications(SaveQualification qualification)
    {
        var token = await _accountService.GetValidAccessTokenAsync();

        var json = JsonSerializer.Serialize(qualification);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        _httpClient.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        var response = await _httpClient.PostAsync("qualifications", content);

        return response.StatusCode;
    }

    public async Task<HttpStatusCode> DeleteQualification(Guid qualificationId)
    {
        var token = await _accountService.GetValidAccessTokenAsync();

        _httpClient.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        var response = await _httpClient.DeleteAsync($"qualifications/{qualificationId}");

        return response.StatusCode;
    }
}
