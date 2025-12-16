using Application.Auth.Common;
using Application.Interfaces;
using Domain.Tokens;
using Domain.Users;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Shared;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Infrastructure.Auth;

public class AuthService : IAuthService
{
    private readonly ITokenRepository _tokenRepository;
    private readonly IConfiguration _configuration;
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;

    public AuthService(
        ITokenRepository tokenRepository, 
        IConfiguration configuration, 
        IUserRepository userRepository, 
        IPasswordHasher passwordHasher)
    {
        _tokenRepository = tokenRepository;
        _configuration = configuration;
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
    }

    public string GenerateRefreshToken()
    {
        var bytes = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(bytes);
        return Convert.ToBase64String(bytes);
    }

    public string GenerateToken(Guid userId, string name)
    {
        var key = _configuration["JwtSettings:Key"]!;
        var keyBytes = Encoding.UTF8.GetBytes(key);

        var expirationMinutes = int.Parse(
            _configuration["JwtSettings:ExpiryTime"] ?? "15"
        );

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, name)
            }),
            Expires = DateTime.UtcNow.AddMinutes(expirationMinutes),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(keyBytes),
                SecurityAlgorithms.HmacSha256Signature
            )
        };

        var handler = new JwtSecurityTokenHandler();
        var token = handler.CreateToken(tokenDescriptor);
        return handler.WriteToken(token);
    }

    public async Task SaveRefreshToken(Guid userId, string refreshToken)
    {
        var expiryTime = DateTime.UtcNow.AddDays(7);
        await _tokenRepository.SaveOrUpdateTokenAsync(userId, refreshToken, expiryTime);
    }

    public async Task<Result<LoginResponse>> LoginAsync(string email, string password)
    {
        var user = await _userRepository.GetUserByEmail(email);

        if (user is null)
            return Result.Failure<LoginResponse>(UserErrors.UserNotFoundByEmail(email));

        if (!_passwordHasher.Verify(password, user.Password.ToString()))
            return Result.Failure<LoginResponse>(UserErrors.LoginFailure());

        var accessToken = GenerateToken(user.Id, user.Name);
        var refreshToken = GenerateRefreshToken();

        await SaveRefreshToken(user.Id, refreshToken);

        return new LoginResponse(accessToken, refreshToken);
    }
    public async Task<Result<LoginResponse>> RefreshTokenAsync(string refreshToken)
    {
        if (string.IsNullOrWhiteSpace(refreshToken))
            return Result.Failure<LoginResponse>(UserErrors.LoginFailure());

        var storedToken = await _tokenRepository.GetToken(refreshToken);

        if (storedToken == null)
            return Result.Failure<LoginResponse>(UserErrors.LoginFailure());

        var user = await _userRepository.GetUserById(storedToken.UserId);

        if (user == null)
            return Result.Failure<LoginResponse>(UserErrors.UserNotFound());

        var newAccessToken = GenerateToken(user.Id, user.Name);
        var newRefreshToken = GenerateRefreshToken();

        await _tokenRepository.SaveOrUpdateTokenAsync(user.Id, newRefreshToken, DateTime.UtcNow.AddDays(7));

        return new LoginResponse(newAccessToken,newRefreshToken);
    }
}
