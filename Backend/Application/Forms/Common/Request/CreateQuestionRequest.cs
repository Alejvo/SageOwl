namespace Application.Forms.Common.Request;

public record CreateQuestionRequest(
    string Title,
    string? Description,
    string QuestionType,
    List<CreateOptionRequest>? Options
    );
