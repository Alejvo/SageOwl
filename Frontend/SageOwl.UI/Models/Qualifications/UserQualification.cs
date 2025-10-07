namespace SageOwl.UI.Models.Qualifications;

public class UserQualification
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Name { get; set; }
    public double Grade { get; set; }
    public int Position { get; set; }
    public bool HasValue { get; set; }
    public string? Description {  get; set; }
}
