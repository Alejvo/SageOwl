using Application.Abstractions;

namespace Application.Users.Commands.Update;

public record UpdateUserCommand(
    Guid Id,
    string Name,
    string Surname,
    string Email,
    string Password,
    string Username,
    DateTime Birthday
    ) : ICommand;
