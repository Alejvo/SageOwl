using SageOwl.UI.ViewModels.Announcements;

namespace SageOwl.UI.ViewModels.Teams.UI;

public class TeamAnnouncementsPageViewModel
{
    public Guid TeamId { get; set; }
    public List<AnnouncementViewModel> Announcements { get; set; } = [];
}
