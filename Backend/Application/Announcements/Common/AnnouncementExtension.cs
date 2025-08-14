using Domain.Announcements;

namespace Application.Announcements.Common;

public static class AnnouncementExtension
{
    public static AnnouncementResponse ToAnnouncementResponse(this Announcement announcement)
    {
        return new AnnouncementResponse(
            announcement.Title,
            announcement.Content,
            announcement.CreatedAt,
            announcement.Author.Name + " " + announcement.Author.Surname);
    }
}
