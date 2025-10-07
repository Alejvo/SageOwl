namespace SageOwl.UI.ViewModels.Qualifications;

public class UserQualificationViewModel
{
    public List<double> Grades { get; set; }
    public List<int> Positions { get; set; }
    public Guid UserId { get; set; }
    public string Name { get; set; }
}
