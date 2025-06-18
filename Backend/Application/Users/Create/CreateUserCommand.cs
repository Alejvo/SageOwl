using Application.Abstractions;

namespace Application.Users.Create;

public record CreateUserCommand(
    string Name,
    string Surname,
    string Email,
    string Password,
    string Username,
    DateTime Birthday
    ) : ICommand;