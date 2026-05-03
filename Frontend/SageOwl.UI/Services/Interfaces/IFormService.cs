using SageOwl.UI.Models.Forms;
using SageOwl.UI.ViewModels.Forms.Create;
using SageOwl.UI.ViewModels.Forms.Update;
using System.Net;

namespace SageOwl.UI.Services.Interfaces;

public interface IFormService
{
    Task<List<Form>> GetFormsByUserId(Guid userId);
    Task<List<Form>> GetFormsByTeamId(Guid teamId);
    Task<Form> GetFormById(Guid formId);
    Task<HttpStatusCode> UpdateForm(UpdateFormViewModel updateForm);
    Task<HttpStatusCode> CreateForm(CreateFormViewModel createForm);
    Task<HttpStatusCode> DeleteForm(Guid formId);
}
