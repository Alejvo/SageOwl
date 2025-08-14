namespace Domain.Announcements;

public interface IAnnouncementRepository
{
    Task<bool> CreateAnnouncement(Announcement announcement);
    Task<List<Announcement>> GetAnnouncementsByTeamId(Guid teamId);
    Task<List<Announcement>> GetAnnouncements();
}
