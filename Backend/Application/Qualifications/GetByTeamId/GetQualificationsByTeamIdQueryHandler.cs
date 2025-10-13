using Application.Abstractions;
using Application.Qualifications.Common;
using Domain.Qualifications;
using Shared;

namespace Application.Qualifications.GetByTeamId;

internal sealed class GetQualificationsByTeamIdQueryHandler : IQueryHandler<GetQualificationsByTeamIdQuery, IEnumerable<QualificationResponse>>
{
    private readonly IQualificationRepository _qualificationRepository;

    public GetQualificationsByTeamIdQueryHandler(IQualificationRepository qualificationRepository)
    {
        _qualificationRepository = qualificationRepository;
    }

    public async Task<Result<IEnumerable<QualificationResponse>>> Handle(GetQualificationsByTeamIdQuery request, CancellationToken cancellationToken)
    {
        var qualification = await _qualificationRepository.GetQualificationByTeamId(request.TeamId);

        if (qualification == null)
            return Result.Failure<IEnumerable<QualificationResponse>>(Error.DBFailure);

        return qualification
            .Select(q => q.ToResponse())
            .ToList();
    }
}
