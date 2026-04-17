using Application.Abstractions;
using Application.Qualifications.Common;

namespace Application.Qualifications.Queries.GetByTeamId;

public record  GetQualificationsByTeamIdQuery(
    Guid TeamId
    ) : IQuery<IEnumerable<QualificationResponse>>;