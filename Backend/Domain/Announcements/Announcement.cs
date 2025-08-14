using Domain.Users;

namespace Domain.Announcements;

public class Announcement
{
    public Guid Id {  get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public Guid AuthorId { get; set; }
    public User Author { get; set; }
    public Guid TeamId { get; set; }

    private Announcement(Guid id,string title, string content, DateTime createdAt ,Guid authorId, Guid teamId)
    {
        Id = id;
        Title = title;
        Content = content;
        CreatedAt = createdAt;
        AuthorId = authorId;
        TeamId = teamId;
    }

    private Announcement() { }

    public static Announcement Create(string title, string content, Guid authorId, Guid teamId)
    {
        return new Announcement(Guid.NewGuid(),title,content,DateTime.UtcNow,authorId,teamId);
    }
}
