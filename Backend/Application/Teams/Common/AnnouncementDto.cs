namespace Application.Teams.Common;

public record AnnouncementDto(
    string Title,
    string Content,
    DateTime CreatedAt,
    string Author
);
