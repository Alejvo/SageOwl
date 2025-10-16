using Newtonsoft.Json.Linq;
using SageOwl.UI.Models;
using SageOwl.UI.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Text.Json;

namespace SageOwl.UI.Services.Implementations;

public class FormService : IFormService
{
    private readonly HttpClient _httpClient;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IAccountService _accountService;

    public FormService(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor, IAccountService accountService)
    {
        _httpClient = httpClientFactory.CreateClient("Backend");
        _httpContextAccessor = httpContextAccessor;
        _accountService = accountService;
    }

    public async Task<List<Form>> GetFormsByUserId()
    {

        var token = await _accountService.GetValidAccessTokenAsync();

        if (string.IsNullOrEmpty(token))
            throw new Exception("Token was not found");

        var handler = new JwtSecurityTokenHandler();
        var jwtToken = handler.ReadJwtToken(token);

        var userIdClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "sub");
        var userId = userIdClaim?.Value;

        var request = new HttpRequestMessage(HttpMethod.Get, $"form/userId/{userId}");
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await _httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<List<Form>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
               ?? new List<Form>();
    }

}
