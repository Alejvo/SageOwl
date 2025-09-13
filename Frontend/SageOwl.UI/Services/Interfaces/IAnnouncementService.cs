using SageOwl.UI.Models;

namespace SageOwl.UI.Services.Interfaces;

public interface IAnnouncementService
{
    Task<List<Announcement>> GetAnnouncements();
    Task<List<Announcement>> GetAnnouncementsByTeamId(Guid teamId);
}
