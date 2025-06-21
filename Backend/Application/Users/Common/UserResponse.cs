namespace Application.Users.Common;

public record UserResponse(
    Guid Id,
    string Name,
    string Surname,
    string Email,
    string Username,
    DateTime CreatedAt
    );
