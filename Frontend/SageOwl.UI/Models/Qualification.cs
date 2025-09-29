namespace SageOwl.UI.Models;

public class Qualification
{
    public Guid Id { get; set; }
    public Guid TeamId { get; set; }
    public double MinimumGrade { get; set; }
    public double MaximumGrade { get; set; }
    public double PassingGrade { get; set; }
    public int Period { get; set; }
    public List<UserQualification> UserQualifications {  get; set; } = new List<UserQualification>();
}
