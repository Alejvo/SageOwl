namespace SageOwl.UI.Models;

public class Team
{
    public Guid TeamId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public List<Form> Forms { get; set; } = new List<Form>();
    public List<Announcement> Announcements { get; set; } = new List<Announcement>();
}
