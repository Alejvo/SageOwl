using SageOwl.UI.ViewModels.Teams;

namespace SageOwl.UI.Models;

public class UpdateTeamDto
{
    public Guid TeamId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public List<MemberViewModel> Members { get; set; }
}
