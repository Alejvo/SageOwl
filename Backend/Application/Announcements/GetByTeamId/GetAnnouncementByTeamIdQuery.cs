using Application.Abstractions;
using Application.Announcements.Common;

namespace Application.Announcements.GetByTeamId;

public record GetAnnouncementByTeamIdQuery(
        Guid TeamId
    ) : IQuery<List<AnnouncementResponse>>;
