namespace Domain.Announcements;

public interface IAnnouncementRepository
{
    Task CreateAnnouncement(Announcement announcement);
    Task<List<Announcement>> GetAnnouncementsByTeamId(Guid teamId);
    Task<List<Announcement>> GetAnnouncements();
}
