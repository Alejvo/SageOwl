using SageOwl.UI.Models.FormSubmissions;
using SageOwl.UI.Services.Interfaces;
using SageOwl.UI.ViewModels.FormSubmissions;
using System.Net;
using System.Text;
using System.Text.Json;

namespace SageOwl.UI.Services.Implementations;

public class FormSubmissionService : IFormSubmissionService
{
    private readonly HttpClient _httpClient;

    public FormSubmissionService(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("Backend");
    }

    public async Task<HttpStatusCode> CreateFormSubmission(CreateFormSubmissionViewModel createFormSubmission)
    {
        var json = JsonSerializer.Serialize(createFormSubmission);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync("form", content);

        return response.StatusCode;
    }

    public async Task<List<FormSubmission>?> GetSubmissionsByFormId(Guid formId)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, $"formSubmissions/formId/{formId}");

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
