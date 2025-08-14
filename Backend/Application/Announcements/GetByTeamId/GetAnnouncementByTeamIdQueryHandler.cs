using Application.Abstractions;
using Application.Announcements.Common;
using Domain.Announcements;
using Shared;

namespace Application.Announcements.GetByTeamId;

internal sealed class GetAnnouncementByTeamIdQueryHandler : IQueryHandler<GetAnnouncementByTeamIdQuery, List<AnnouncementResponse>>
{
    private readonly IAnnouncementRepository _announcementRepository;

    public GetAnnouncementByTeamIdQueryHandler(IAnnouncementRepository announcementRepository)
    {
        _announcementRepository = announcementRepository;
    }

    public async  Task<Result<List<AnnouncementResponse>>> Handle(GetAnnouncementByTeamIdQuery request, CancellationToken cancellationToken)
    {
        var announcements = await _announcementRepository.GetAnnouncementsByTeamId(request.TeamId);
        return announcements.Select(s => s.ToAnnouncementResponse()).ToList();
    }
}
