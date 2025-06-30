using Application.Abstractions;
using Domain.Tokens;
using Domain.Users;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Application.Users.Services;

public class AuthService : IAuthService
{
    private readonly ITokenRepository _tokenRepository;
    private readonly IConfiguration _configuration;
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;

    public AuthService(ITokenRepository tokenRepository, IConfiguration configuration, IUserRepository userRepository, IPasswordHasher passwordHasher)
    {
        _tokenRepository = tokenRepository;
        _configuration = configuration;
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
    }

    public string GenerateRefreshToken()
    {
        var byteArray = new byte[64];
        string refreshToken;
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(byteArray);
        refreshToken = Convert.ToBase64String(byteArray);
        return refreshToken;
    }

    public string GenerateToken(Guid userId, string name)
    {
        var key = _configuration.GetSection("JwtSettings")["Key"];
        var keyBytes = Encoding.ASCII.GetBytes(key!);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(
            [
                new Claim(JwtRegisteredClaimNames.Sub,userId.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName,name)
            ]),
            Expires = DateTime.UtcNow.AddDays(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256Signature)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public async Task SaveRefreshToken(Guid userId, string refreshToken)
    {
        var user = await _userRepository.GetUserById(userId);
        if (user != null)
        {
            var token = Token.Create(refreshToken, userId);
            await _tokenRepository.SaveToken(token.UserId, token.JwtToken, token.Id, token.ExpiryTime);
        }
    }

    public async Task<(string AccessToken, string RefreshToken)> LoginAsync(string email, string password)
    {
        var user = await _userRepository.GetUserByEmail(email);
        if (user is null || !_passwordHasher.Verify(password, user.Password.ToString()))
            throw new UnauthorizedAccessException("Invalid credentials.");

        var accessToken = GenerateToken(user.Id, user.Name);
        var refreshToken = GenerateRefreshToken();

        await SaveRefreshToken(user.Id, refreshToken);

        return (accessToken, refreshToken);
    }
}
