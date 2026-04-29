namespace SageOwl.UI.ViewModels.Qualifications;

public class QualificationViewModel
{
    public Guid QualificationId { get; set; }
    public string Period { get; set; }
    public int TotalGrades { get; set; }
    public List<string> Descriptions { get; set; } = [];
    public List<UserQualificationViewModel> UserQualifications { get; set; } = [];
}
