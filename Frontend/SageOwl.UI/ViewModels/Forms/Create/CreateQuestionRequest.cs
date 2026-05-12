namespace SageOwl.UI.ViewModels.Forms.Create;

public record CreateQuestionRequest(
    string Text,
    string QuestionType,
    List<CreateOptionRequest>? Options = null
    );
