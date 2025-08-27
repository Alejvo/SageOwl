namespace SageOwl.UI.ViewModels.Announcements;

public class AnnouncementViewModel
{
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public string PublisherName { get; set; } = string.Empty;
    public DateTime SentAt { get; set; }
}
