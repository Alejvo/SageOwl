namespace SageOwl.UI.ViewModels.Qualifications;

public class SaveQualificationsViewModel
{
    public Guid TeamId { get; set; }
    public double MinimumGrade { get; set; }
    public double MaximumGrade { get; set; }
    public double PassingGrade { get; set; }
    public string Period { get; set; }
    public int TotalGrades { get; set; }
    public List<string> Descriptions { get; set; } = new List<string>();

    public List<SaveUserQualificationViewModel> UserQualifications { get; set; } = new List<SaveUserQualificationViewModel>();
}
