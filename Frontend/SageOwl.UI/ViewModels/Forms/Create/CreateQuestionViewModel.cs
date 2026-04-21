namespace SageOwl.UI.ViewModels.Forms.Create;

public record CreateQuestionViewModel(
    string Text,
    string QuestionType,
    List<CreateOptionViewModel>? Options = null
    );
