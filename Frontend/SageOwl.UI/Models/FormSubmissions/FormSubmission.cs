namespace SageOwl.UI.Models.FormSubmissions;

public class FormSubmission
{
    public Guid FormId { get; set; }
    public DateTime SubmittedAt { get; set; }
    public IEnumerable<Answer> Answers { get; set; }
}
