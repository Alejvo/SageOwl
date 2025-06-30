﻿namespace Application.Abstractions;
public interface IAuthService
{
    string GenerateToken(Guid userId, string name);
    string GenerateRefreshToken();
    Task SaveRefreshToken(Guid userId, string refreshToken);
    Task<(string AccessToken, string RefreshToken)> LoginAsync(string email, string password);
}
