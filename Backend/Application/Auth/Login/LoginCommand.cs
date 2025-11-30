using Application.Abstractions;
using Application.Auth.Common;

namespace Application.Auth.Login;

public record LoginCommand(
    string Email,
    string Password
    ) : ICommand<LoginResponse>;

