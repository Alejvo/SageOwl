using SageOwl.UI.Models.FormSubmissions;
using SageOwl.UI.ViewModels.Forms.Create;
using SageOwl.UI.ViewModels.FormSubmissions;
using System.Net;

namespace SageOwl.UI.Services.Interfaces;

public interface IFormSubmissionService
{
    Task<HttpStatusCode> CreateFormSubmission(CreateFormSubmissionViewModel createFormSubmission);
    Task<List<FormSubmission>?> GetSubmissionsByFormId(Guid formId);
}
