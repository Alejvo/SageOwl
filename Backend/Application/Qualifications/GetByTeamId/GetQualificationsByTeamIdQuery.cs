using Application.Abstractions;
using Application.Qualifications.Common;

namespace Application.Qualifications.GetByTeamId;

public record  GetQualificationsByTeamIdQuery(
    Guid TeamId
    ) : IQuery<List<QualificationResponse>>;