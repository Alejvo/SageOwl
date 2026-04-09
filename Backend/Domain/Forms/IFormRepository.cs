namespace Domain.Forms;

public interface IFormRepository
{
    Task<Form?> GetFormById(Guid formId);
    Task<List<Form>> GetPendingFormsByUserId(Guid userId);
    Task<List<Form>> GetByTeamId(Guid teamId);
    Task<List<FormResult>> GetFormResults(Guid formId);
    Task CreateForm(Form form);
    Task DeleteForm(Form form);
    Task CreateFormResult(FormResult form);
}
