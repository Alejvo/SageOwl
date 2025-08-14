using Application.Abstractions;
using Application.Announcements.Common;

namespace Application.Announcements.GetAll;

public record GetAnnouncementsQuery(
    ):IQuery<List<AnnouncementResponse>>;
