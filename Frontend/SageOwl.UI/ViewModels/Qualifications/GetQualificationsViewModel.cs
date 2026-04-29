namespace SageOwl.UI.ViewModels.Qualifications;

public class GetQualificationsViewModel
{
    public Dictionary<Guid, string> QualificationKeys { get; set; } = [];
    public Guid TeamId { get; set; }
    public List<QualificationViewModel> Qualifications { get; set; } = new();
}
