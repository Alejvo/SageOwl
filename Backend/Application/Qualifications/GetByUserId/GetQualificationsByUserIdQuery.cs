using Application.Abstractions;
using Application.Qualifications.Common;

namespace Application.Qualifications.GetByUserId;

public record GetQualificationsByUserIdQuery(
    Guid UserId
    ) : IQuery<IEnumerable<QualificationResponse>>;
