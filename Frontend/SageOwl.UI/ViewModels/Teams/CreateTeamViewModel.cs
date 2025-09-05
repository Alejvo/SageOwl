namespace SageOwl.UI.ViewModels.Teams;

public class CreateTeamViewModel
{
    public string Name { get; set; }
    public string Description { get; set; }
    public List<MemberViewModel> Members { get; set; }
    public string searchTerm { get; set; }
}
