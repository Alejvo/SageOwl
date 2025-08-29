using Application.Abstractions;
using Application.Forms.Common.Response;

namespace Application.Forms.GetByUserId;

public record GetPendingFormsByUserIdQuery(
    Guid UserId
    ) : IQuery<List<FormResponse>>;
