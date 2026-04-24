using Newtonsoft.Json.Linq;
using SageOwl.UI.Models;
using SageOwl.UI.Services.Interfaces;
using SageOwl.UI.ViewModels.Forms.Create;
using SageOwl.UI.ViewModels.Forms.Update;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
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

    public async Task<HttpStatusCode> CreateForm(CreateFormViewModel createForm)
    {
        var token = await _accountService.GetValidAccessTokenAsync();

        var json = JsonSerializer.Serialize(createForm);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);

        var response = await _httpClient.PostAsync("form", content);

        return response.StatusCode;
    }

    public async Task<HttpStatusCode> DeleteForm(Guid formId)
    {
        var token = await _accountService.GetValidAccessTokenAsync();

        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);

        var response = await _httpClient.DeleteAsync($"form/{formId}");

        return response.StatusCode;
    }

    public async Task<Form> GetFormById(Guid formId)
    {
        var token = await _accountService.GetValidAccessTokenAsync();

        var request = new HttpRequestMessage(HttpMethod.Get, $"form/{formId}");
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await _httpClient.SendAsync(request);

        if (!response.IsSuccessStatusCode)
            return null;

        var content = await response.Content.ReadAsStringAsync();

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        var form = JsonSerializer.Deserialize<Form>(content, options);
        return form;
    }

    public async Task<List<Form>> GetFormsByTeamId(Guid teamId)
    {
        var token = await _accountService.GetValidAccessTokenAsync();

        var request = new HttpRequestMessage(HttpMethod.Get, $"form/teamId/{teamId}");
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await _httpClient.SendAsync(request);

        if (!response.IsSuccessStatusCode)
            return null;

        var content = await response.Content.ReadAsStringAsync();

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        var forms = JsonSerializer.Deserialize<List<Form>>(content, options);
        return forms;
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

        if(response.StatusCode == HttpStatusCode.OK)
        {
            var content = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<List<Form>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
        return new List<Form>();
    }

    public async Task<HttpStatusCode> UpdateForm(UpdateFormViewModel updateForm)
    {
        var token = await _accountService.GetValidAccessTokenAsync();

        var json = JsonSerializer.Serialize(updateForm);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);

        var response = await _httpClient.PutAsync("form", content);

        return response.StatusCode;
    }
}
