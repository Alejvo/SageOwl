namespace Application.FormSubmissions.Common.Request;

public record CreateAnswerRequest(
    Guid QuestionId,
    string Value
    );
