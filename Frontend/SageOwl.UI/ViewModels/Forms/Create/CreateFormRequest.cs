namespace SageOwl.UI.ViewModels.Forms.Create;

public record CreateFormRequest(
    string Title,
    string TeamId,
    DateTime Deadline,
    List<CreateQuestionRequest> Questions
    );
