using SageOwl.UI.ViewModel;
using System.Text;
using System.Text.Json;

namespace SageOwl.UI.Services;

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
}
