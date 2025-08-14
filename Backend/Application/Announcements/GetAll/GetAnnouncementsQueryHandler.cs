using Application.Abstractions;
using Application.Announcements.Common;
using Domain.Announcements;
using Shared;

namespace Application.Announcements.GetAll;

internal sealed class GetAnnouncementsQueryHandler : IQueryHandler<GetAnnouncementsQuery, List<AnnouncementResponse>>
{
    private readonly IAnnouncementRepository _announcementRepository;

    public GetAnnouncementsQueryHandler(IAnnouncementRepository announcementRepository)
    {
        _announcementRepository = announcementRepository;
    }

    public async Task<Result<List<AnnouncementResponse>>> Handle(GetAnnouncementsQuery request, CancellationToken cancellationToken)
    {
        var announcements = await _announcementRepository.GetAnnouncements();
        return announcements.Select(s => s.ToAnnouncementResponse()).ToList();
    }
}
