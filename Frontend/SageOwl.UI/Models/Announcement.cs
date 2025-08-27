namespace SageOwl.UI.Models;

public class Announcement
{
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public string Author { get; set; } = string.Empty;
}
