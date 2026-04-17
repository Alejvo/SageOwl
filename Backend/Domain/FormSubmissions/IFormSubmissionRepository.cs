namespace Domain.FormSubmissions;

public interface IFormSubmissionRepository
{
    Task<List<FormSubmission>> GetFormSubmissionsByForm(Guid formId);

    Task CreateFormSubmission(FormSubmission formSubmission); 
}
