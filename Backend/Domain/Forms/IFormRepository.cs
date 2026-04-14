namespace Domain.Forms;

public interface IFormRepository
{
    Task<Form?> GetFormById(Guid formId);
    Task<List<Form>> GetByTeamId(Guid teamId);
    Task CreateForm(Form form);
    Task DeleteForm(Form form);
}
