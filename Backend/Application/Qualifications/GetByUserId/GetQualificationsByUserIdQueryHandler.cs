using Application.Abstractions;
using Application.Qualifications.Common;
using Domain.Qualifications;
using Shared;

namespace Application.Qualifications.GetByUserId;

internal sealed class GetQualificationsByUserIdQueryHandler : IQueryHandler<GetQualificationsByUserIdQuery, IEnumerable<QualificationResponse>>
{
    private readonly IQualificationRepository _qualificationRepository;

    public GetQualificationsByUserIdQueryHandler(IQualificationRepository qualificationRepository)
    {
        _qualificationRepository = qualificationRepository;
    }

    public async Task<Result<IEnumerable<QualificationResponse>>> Handle(GetQualificationsByUserIdQuery request, CancellationToken cancellationToken)
    {
        var qualifications = await _qualificationRepository.GetQualificationsByUserId(request.UserId);

        return qualifications.Select(q => q.ToResponse()).ToList();
    }
}
