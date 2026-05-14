namespace SageOwl.UI.ViewModels.Teams;

public class GetTeamViewModel
{
    public Guid TeamId { get; set; }
    public string Name { get; set; } = string.Empty;

    public bool IsAdmin { get; set; }

    public string Url { get; set; } = string.Empty;
    public string Tooltip { get; set; } = string.Empty;
}
