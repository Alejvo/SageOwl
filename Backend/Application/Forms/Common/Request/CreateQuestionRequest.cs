namespace Application.Forms.Common.Request;

public record CreateQuestionRequest(
    string Text,
    string QuestionType,
    List<CreateOptionRequest>? Options = null
    );
