using SageOwl.UI.Models;
using SageOwl.UI.ViewModels.Announcements;

namespace SageOwl.UI.Services.Interfaces;

public interface IAnnouncementService
{
    Task<List<Announcement>> GetAnnouncements();
    Task<List<Announcement>> GetAnnouncementsByTeamId(Guid teamId);
    Task<bool> CreateAnnouncement(CreateAnnouncementViewModel createAnnouncement);
}
