using SageOwl.UI.Models;
using SageOwl.UI.Services.Interfaces;
using System.Text.Json;

namespace SageOwl.UI.Services.Implementations;

public class AnnouncementService : IAnnouncementService
{
    private readonly HttpClient _httpClient;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AnnouncementService(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)
    {
        _httpClient = httpClientFactory.CreateClient("Backend");
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<List<Announcement>> GetAnnouncements()
    {
        var token = _httpContextAccessor.HttpContext?.Request.Cookies["AccessToken"];

        var request = new HttpRequestMessage(HttpMethod.Get, $"announcements");
        request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        var response = await _httpClient.SendAsync(request);

        if (!response.IsSuccessStatusCode)
            return null;

        var content = await response.Content.ReadAsStringAsync();

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        var announcements = JsonSerializer.Deserialize<List<Announcement>>(content, options);
        return announcements;
    }

    public async Task<List<Announcement>> GetAnnouncementsByTeamId(Guid teamId)
    {
        var token = _httpContextAccessor.HttpContext?.Request.Cookies["AccessToken"];

        var request = new HttpRequestMessage(HttpMethod.Get, $"announcements/teamId/{teamId}");
        request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        var response = await _httpClient.SendAsync(request);

        if (!response.IsSuccessStatusCode)
            return null;

        var content = await response.Content.ReadAsStringAsync();

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        var announcements = JsonSerializer.Deserialize<List<Announcement>>(content, options);
        return announcements;
    }
}
