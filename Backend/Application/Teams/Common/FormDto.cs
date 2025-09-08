namespace Application.Teams.Common;

public record FormDto(
    Guid Id,
    string Title,
    DateTime Deadline
);
