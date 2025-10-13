namespace SageOwl.UI.ViewModels.Qualifications;

public class QualificationViewModel
{
    public List<string> PeriodList { get; set; }
    public string Period { get; set; }
    public int TotalGrades { get; set; }
    public List<string> Teams { get; set; }
    public List<string> Descriptions { get; set; } = new List<string>();
    public List<UserQualificationViewModel> UserQualifications { get; set; }
}
