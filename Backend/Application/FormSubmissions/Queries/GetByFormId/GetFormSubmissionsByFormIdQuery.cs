using Application.Abstractions;
using Application.FormSubmissions.Common.Response;

namespace Application.FormSubmissions.Queries.GetByFormId;

public record GetFormSubmissionsByFormIdQuery(
    Guid FormId
    ) : IQuery<IEnumerable<FormSubmissionResponse>>;
