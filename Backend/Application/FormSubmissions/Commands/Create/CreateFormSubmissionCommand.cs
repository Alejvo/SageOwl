using Application.Abstractions;
using Application.FormSubmissions.Common.Request;
using Application.FormSubmissions.Common.Response;

namespace Application.FormSubmissions.Commands.Create;

public record CreateFormSubmissionCommand(
    Guid FormId,
    Guid UserId,
    DateTime SubmittedAt,
    IEnumerable<CreateAnswerRequest> Answers
    ): ICommand;
