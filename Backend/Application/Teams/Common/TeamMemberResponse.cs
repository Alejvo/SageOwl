namespace Application.Teams.Common;

public record TeamMemberResponse(
    Guid Id,
    string Name,
    string Surname,
    string Email,
    string Username,
    DateTime CreatedAt,
    string Role
    );
