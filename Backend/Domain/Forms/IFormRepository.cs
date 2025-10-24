namespace Domain.Forms;

public interface IFormRepository
{
    Task<Form?> GetFormById(Guid formId);
    Task<List<Form>> GetPendingFormsByUserId(Guid userId);
    Task<List<Form>> GetByTeamId(Guid teamId);
    Task<FormResult> GetFormResults(Guid formId);
    Task<bool> CreateForm(Form form);
    Task<bool> UpdateForm(Form form);
}
