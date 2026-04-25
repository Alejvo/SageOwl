namespace SageOwl.UI.Models.Qualifications;

public class SaveUserQualification
{
    public Guid UserId { get; set; }
    public double Grade { get; set; }
    public string? Description { get; set; }
}
