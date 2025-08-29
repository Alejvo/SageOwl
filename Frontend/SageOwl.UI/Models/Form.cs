namespace SageOwl.UI.Models;

public class Form
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public Guid TeamId { get; set; }
    public DateTime Deadline { get; set; }
}
