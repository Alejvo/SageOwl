using Application.FormSubmissions.Common.Response;
using Domain.FormSubmissions;

namespace Application.FormSubmissions.Common;

public static class FormSubmissionExtensions
{
    public static FormSubmissionResponse ToResponse(this FormSubmission formSubmission)
    {
        return new FormSubmissionResponse(
            formSubmission.FormId,
            formSubmission.SubmittedAt,
            formSubmission.Answers.Select(a => a.ToResponse())
            );
    }

    public static AnswerResponse ToResponse(this Answer answer)
    {
        return new AnswerResponse(answer.Value);
    }
}
