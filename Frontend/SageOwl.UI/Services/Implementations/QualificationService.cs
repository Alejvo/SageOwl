using SageOwl.UI.Models.Qualifications;
using SageOwl.UI.Services.Interfaces;
using System.Net;
using System.Text;
using System.Text.Json;

namespace SageOwl.UI.Services.Implementations;

public class QualificationService : IQualificationService
{
    private readonly HttpClient _httpClient;

    public QualificationService(
        IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("Backend");
    }
    public async Task<List<Qualification>> GetQualificationByUserId(Guid userId)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, $"qualifications/userId/{userId}");

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
        var request = new HttpRequestMessage(HttpMethod.Get, $"qualifications/teamId/{teamId}");

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
        var json = JsonSerializer.Serialize(qualification);
        var content = new StringContent(json, Encoding.UTF8, "application/json");


        var response = await _httpClient.PostAsync("qualifications", content);

        return response.StatusCode;
    }

    public async Task<HttpStatusCode> DeleteQualification(Guid qualificationId)
    {
        var response = await _httpClient.DeleteAsync($"qualifications/{qualificationId}");

        return response.StatusCode;
    }
}
