namespace SageOwl.UI.Models;

public class CurrentTeam
{
    public Guid TeamId {  get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public List<Form> Forms { get; set; } = new List<Form>();
    public List<Announcement> Announcements { get; set; } = new List<Announcement>();

}
