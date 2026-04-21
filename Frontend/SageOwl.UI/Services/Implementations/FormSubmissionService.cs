using SageOwl.UI.Models.FormSubmissions;
using SageOwl.UI.Services.Interfaces;
using SageOwl.UI.ViewModels.FormSubmissions;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace SageOwl.UI.Services.Implementations;

public class FormSubmissionService : IFormSubmissionService
{
    private readonly HttpClient _httpClient;
    private readonly IAccountService _accountService;

    public FormSubmissionService(IHttpClientFactory httpClientFactory, IAccountService accountService)
    {
        _httpClient = httpClientFactory.CreateClient("Backend");
        _accountService = accountService;
    }

    public async Task<HttpStatusCode> CreateFormSubmission(CreateFormSubmissionViewModel createFormSubmission)
    {
        var token = await _accountService.GetValidAccessTokenAsync();

        var json = JsonSerializer.Serialize(createFormSubmission);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);

        var response = await _httpClient.PostAsync("form", content);

        return response.StatusCode;
    }

    public async Task<List<FormSubmission>?> GetSubmissionsByFormId(Guid formId)
    {
        var token = await _accountService.GetValidAccessTokenAsync();

        var request = new HttpRequestMessage(HttpMethod.Get, $"formSubmissions/formId/{formId}");
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await _httpClient.SendAsync(request);

        if (!response.IsSuccessStatusCode)
            return null;

        var content = await response.Content.ReadAsStringAsync();

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        var submissions = JsonSerializer.Deserialize<List<FormSubmission>>(content, options);
        return submissions;
    }
}
