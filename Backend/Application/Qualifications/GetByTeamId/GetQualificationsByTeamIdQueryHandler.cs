using Application.Abstractions;
using Application.Qualifications.Common;
using Domain.Qualifications;
using Shared;

namespace Application.Qualifications.GetByTeamId;

internal sealed class GetQualificationsByTeamIdQueryHandler : IQueryHandler<GetQualificationsByTeamIdQuery, List<QualificationResponse>>
{
    private readonly IQualificationRepository _qualificationRepository;

    public GetQualificationsByTeamIdQueryHandler(IQualificationRepository qualificationRepository)
    {
        _qualificationRepository = qualificationRepository;
    }

    public async Task<Result<List<QualificationResponse>>> Handle(GetQualificationsByTeamIdQuery request, CancellationToken cancellationToken)
    {
        var qualifications = await _qualificationRepository.GetQualificationsByTeamId(request.TeamId);

        return qualifications.Select(q => q.ToResponse()).ToList();
    }
}
