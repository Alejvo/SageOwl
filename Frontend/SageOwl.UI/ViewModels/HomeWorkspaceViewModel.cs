using SageOwl.UI.Models;
using SageOwl.UI.ViewModels.Announcements;
using SageOwl.UI.ViewModels.Forms;

namespace SageOwl.UI.ViewModels;

public class HomeWorkspaceViewModel
{
    public List<FormViewModel> Forms { get; set; }
    public List<AnnouncementViewModel> Announcements { get; set; }
}
