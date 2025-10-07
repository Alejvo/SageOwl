namespace SageOwl.UI.Models.Qualifications;

public class Qualification
{
    public Guid Id { get; set; }
    public Guid TeamId { get; set; }
    public double MinimumGrade { get; set; }
    public double MaximumGrade { get; set; }
    public double PassingGrade { get; set; }
    public string Period { get; set; }
    public int TotalGrades { get; set; }
    public List<UserQualification> UserQualifications {  get; set; } = new List<UserQualification>();
}
