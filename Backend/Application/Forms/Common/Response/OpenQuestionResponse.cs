namespace Application.Forms.Common.Response;

public record OpenQuestionResponse(
    Guid Id,
    string Text
    ) : QuestionResponse(Id,Text,"OPEN");
