using Application.Auth.Common;
using Shared;

namespace Application.Interfaces;
public interface IAuthService
{
    string GenerateToken(Guid userId, string name);
    string GenerateRefreshToken();
    Task SaveRefreshToken(Guid userId, string refreshToken);
    Task<Result<LoginResponse>> LoginAsync(string email, string password);
    Task<Result<LoginResponse>> RefreshTokenAsync(string refreshToken);
}
