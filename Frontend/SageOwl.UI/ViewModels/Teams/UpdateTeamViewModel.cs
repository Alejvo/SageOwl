using SageOwl.UI.Models;

namespace SageOwl.UI.ViewModels.Teams;

public class UpdateTeamViewModel
{
    public Guid TeamId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public List<Member> Members { get; set; } = new List<Member>();
}
