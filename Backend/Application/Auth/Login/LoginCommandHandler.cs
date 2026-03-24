using Application.Abstractions;
using Application.Auth.Common;
using Application.Interfaces;
using Shared;

namespace Application.Auth.Login;

internal sealed class LoginCommandHandler : ICommandHandler<LoginCommand,LoginResponse>
{
    private readonly IAuthService _authService;

    public LoginCommandHandler(IAuthService authService)
    {
        _authService = authService ?? throw new ArgumentNullException(nameof(authService));
    }

    public async Task<Result<LoginResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var result = await _authService.LoginAsync(request.Email, request.Password);
        return result;
    }
}
