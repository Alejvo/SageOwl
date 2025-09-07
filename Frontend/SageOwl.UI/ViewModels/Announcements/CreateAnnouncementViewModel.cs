namespace SageOwl.UI.ViewModels.Announcements;

public class CreateAnnouncementViewModel
{
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public Guid AuthorId { get; set; }
    public Guid TeamId { get; set; }
}
