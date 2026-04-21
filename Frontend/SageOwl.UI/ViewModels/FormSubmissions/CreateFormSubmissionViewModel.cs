namespace SageOwl.UI.ViewModels.FormSubmissions;

public record CreateFormSubmissionViewModel(
    Guid FormId,
    DateTime SubmittedAt,
    IEnumerable<CreateAnswerViewModel> Answers
    );
