using Application.Abstractions;
using Application.Auth.Common;

namespace Application.Auth.RefreshToken;

public record RefreshTokenCommand(
    string RefreshToken
    ) : ICommand<LoginResponse>;
