namespace Domain.FormSubmissions;

public interface IFormSubmissionRepository
{
    List<Task<FormSubmission>> GetFormSubmissionsByForm(Guid formId);

    Task CreateFormSubmission(FormSubmission formSubmission); 
}
