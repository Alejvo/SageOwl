using SageOwl.UI.Models;

namespace SageOwl.UI.Services.Interfaces;

public interface IAnnouncementService
{
    Task<List<Announcement>> GetAnnouncements();
}
