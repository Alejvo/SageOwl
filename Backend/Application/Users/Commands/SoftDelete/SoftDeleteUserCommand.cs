using Application.Abstractions;

namespace Application.Users.Commands.SoftDelete;

public record SoftDeleteUserCommand(
    Guid UserId
    ) : ICommand;
