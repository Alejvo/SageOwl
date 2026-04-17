using Application.Abstractions;
using Application.Qualifications.Common;

namespace Application.Qualifications.Queries.GetByUserId;

public record GetQualificationsByUserIdQuery(
    Guid UserId
    ) : IQuery<IEnumerable<QualificationResponse>>;
