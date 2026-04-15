namespace Application.Forms.Common.Request;

public record UpdateQuestionRequest(
    Guid Id,
    Guid? QuestionId,
    string Title,
    string? Description,
    string QuestionType,
    List<UpdateOptionRequest>? Options
    );
