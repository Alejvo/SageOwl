using SageOwl.UI.ViewModels.Qualifications;

namespace SageOwl.UI.ViewModels.Teams.UI;

public class TeamQualificationsPageViewModel
{
    public Dictionary<Guid, string> QualificationKeys { get; set; } = [];
    public Guid TeamId { get; set; }
    public List<QualificationViewModel> Qualifications { get; set; } = new();
}
