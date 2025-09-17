namespace SageOwl.UI.ViewModels.Teams;

public class TeamCardViewModel
{
    public Guid  TeamId { get; set; }
    public string Initials { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
}
