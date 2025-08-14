using Application.Abstractions;

namespace Application.Announcements.Create;

public record CreateAnnouncementCommand(
    string Title,
    string Content,
    Guid AuthorId,
    Guid Teamid
    ) : ICommand;
