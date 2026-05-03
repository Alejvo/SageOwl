using SageOwl.UI.Models.Forms;
using SageOwl.UI.Services.Interfaces;
using SageOwl.UI.ViewModels.Forms.Create;
using SageOwl.UI.ViewModels.Forms.Update;
using System.Net;
using System.Text;
using System.Text.Json;

namespace SageOwl.UI.Services.Implementations;

public class FormService : IFormService
{
    private readonly HttpClient _httpClient;


    public FormService(
        IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("Backend");
    }

    public async Task<HttpStatusCode> CreateForm(CreateFormViewModel createForm)
    {
        var json = JsonSerializer.Serialize(createForm);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync("form", content);

        return response.StatusCode;
    }

    public async Task<HttpStatusCode> DeleteForm(Guid formId)
    {
        var response = await _httpClient.DeleteAsync($"form/{formId}");

        return response.StatusCode;
    }

    public async Task<Form> GetFormById(Guid formId)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, $"form/{formId}");

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
        var request = new HttpRequestMessage(HttpMethod.Get, $"form/teamId/{teamId}");

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

    public async Task<List<Form>> GetFormsByUserId(Guid userId)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, $"form/userId/{userId}");

        var response = await _httpClient.SendAsync(request);

        if(response.StatusCode == HttpStatusCode.OK)
        {
            var content = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<List<Form>>(
                content, 
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
        return [];
    }

    public async Task<HttpStatusCode> UpdateForm(UpdateFormViewModel updateForm)
    {
        var json = JsonSerializer.Serialize(updateForm);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PutAsync("form", content);

        return response.StatusCode;
    }
}
