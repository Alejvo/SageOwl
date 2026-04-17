using Shared;

namespace Domain.FormSubmissions;

public class FormSubmissionErrors
{
    public static Error FormSubmissionNotFound() => new("FormSubmission.NotFound", $"FormSubmission was not found", ErrorType.NotFound);
}
