namespace Application.Forms.Common.Response;

public record FormResponse(
    Guid Id,
    string Title,
    Guid TeamId,
    DateTime Deadline,
    List<QuestionResponse>? Questions
    );
