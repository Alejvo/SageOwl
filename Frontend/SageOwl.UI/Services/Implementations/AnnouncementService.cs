using SageOwl.UI.Models.Announcements;
using SageOwl.UI.Services.Interfaces;
using SageOwl.UI.ViewModels.Announcements;
using System.Text;
using System.Text.Json;

namespace SageOwl.UI.Services.Implementations;

public class AnnouncementService : IAnnouncementService
{
    private readonly HttpClient _httpClient;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IAuthService _authService;

    public AnnouncementService(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor, IAuthService authService)
    {
        _httpClient = httpClientFactory.CreateClient("Backend");
        _httpContextAccessor = httpContextAccessor;
        _authService = authService;
    }

    public async Task<bool> CreateAnnouncement(CreateAnnouncementViewModel createAnnouncement)
    {
        var json = JsonSerializer.Serialize(createAnnouncement);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync("announcements", content);

        return response.IsSuccessStatusCode;
    }

    public async Task<List<Announcement>> GetAnnouncements()
    {
        var request = new HttpRequestMessage(HttpMethod.Get, $"announcements");

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
        var request = new HttpRequestMessage(HttpMethod.Get, $"announcements/teamId/{teamId}");

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
