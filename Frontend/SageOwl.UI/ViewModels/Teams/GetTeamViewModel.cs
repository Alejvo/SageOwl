using SageOwl.UI.Models;
using SageOwl.UI.Models.Announcements;
using SageOwl.UI.Models.Forms;

namespace SageOwl.UI.ViewModels.Teams;

public class GetTeamViewModel
{
    public Guid TeamId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public List<Form> Forms { get; set; } = [];
    public List<Announcement> Announcements { get; set; } = [];
    public List<Member> Members { get; set; } = [];
    public bool IsAdmin { get; set; }
}
