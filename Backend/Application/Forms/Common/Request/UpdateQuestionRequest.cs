namespace Application.Forms.Common.Request;

public record UpdateQuestionRequest(
    Guid? QuestionId,
    string Title,
    string? Description,
    string QuestionType,
    List<UpdateOptionRequest>? Options,
    bool IsDeleted = false
    );
