using Domain.FormSubmissions;

namespace Application.FormSubmissions.Common.Response;

public record FormSubmissionResponse(
    Guid FormId,
    DateTime SubmittedAt,
    IEnumerable<AnswerResponse> Answers
    );
