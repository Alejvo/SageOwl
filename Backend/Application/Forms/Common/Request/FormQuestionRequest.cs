namespace Application.Forms.Common.Request;

public record FormQuestionRequest(
    string Title,
    string? Description,
    string QuestionType,
    List<FormOptionRequest>? Options
    );
