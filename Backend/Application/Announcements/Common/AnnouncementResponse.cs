namespace Application.Announcements.Common;

public record AnnouncementResponse(
    string Title,
    string Content,
    DateTime CreatedAt,
    string Author
    );
