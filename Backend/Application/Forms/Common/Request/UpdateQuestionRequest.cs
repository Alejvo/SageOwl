namespace Application.Forms.Common.Request;

public record UpdateQuestionRequest(
    Guid? QuestionId,
    string Text,
    string QuestionType,
    List<UpdateOptionRequest>? Options
    );
