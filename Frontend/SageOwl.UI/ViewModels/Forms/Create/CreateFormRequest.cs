namespace SageOwl.UI.ViewModels.Forms.Create;

public record CreateFormRequest(
    string Title,
    Guid TeamId,
    DateTime Deadline,
    List<CreateQuestionRequest> Questions
    );
