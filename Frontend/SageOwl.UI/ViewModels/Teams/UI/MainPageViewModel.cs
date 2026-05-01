using SageOwl.UI.ViewModels.Announcements;
using SageOwl.UI.ViewModels.Forms;

namespace SageOwl.UI.ViewModels.Teams.UI;

public class MainPageViewModel
{
    public Guid TeamId { get; set; }
    public List<AnnouncementViewModel> Announcements { get; set; } = new List<AnnouncementViewModel>();
    public List<FormViewModel> Forms { get; set; } = new List<FormViewModel>();
}
