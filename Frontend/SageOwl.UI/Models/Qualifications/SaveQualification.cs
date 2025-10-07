namespace SageOwl.UI.Models.Qualifications;

public class SaveQualification
{
    public Guid TeamId { get; set; }
    public double MinimumGrade { get; set; }
    public double MaximumGrade { get; set; }
    public double PassingGrade { get; set; }
    public string Period { get; set; }
    public int TotalGrades { get; set; }
    public List<SaveUserQualification> UserQualifications { get; set; } = new List<SaveUserQualification>();
}
