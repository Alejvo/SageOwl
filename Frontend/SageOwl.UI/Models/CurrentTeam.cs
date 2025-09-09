namespace SageOwl.UI.Models;

public class CurrentTeam
{
    public Guid TeamId {  get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public List<Form> Forms { get; set; } = [];
    public List<Announcement> Announcements { get; set; } = [];
    public List<Member> Members { get; set; } = [];
    public bool IsUserAdmin { get; set; } = false;

}
