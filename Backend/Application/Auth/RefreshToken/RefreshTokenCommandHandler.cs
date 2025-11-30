using Application.Abstractions;
using Application.Auth.Common;
using Application.Interfaces;
using Shared;

namespace Application.Auth.RefreshToken;

internal sealed class RefreshTokenCommandHandler : ICommandHandler<RefreshTokenCommand, LoginResponse>
{
    private readonly IAuthService _authService;

    public RefreshTokenCommandHandler(IAuthService authService)
    {
        _authService = authService;
    }

    public async Task<Result<LoginResponse>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        => await _authService.RefreshTokenAsync(request.RefreshToken);
}
