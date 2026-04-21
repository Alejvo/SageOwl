namespace SageOwl.UI.ViewModels.Forms.Create;

public record CreateFormViewModel(
    string Title,
    Guid TeamId,
    DateTime Deadline,
    List<CreateQuestionViewModel> Questions
    );
