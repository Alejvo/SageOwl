namespace Domain.Tokens;

public interface ITokenRepository
{
    Task<Token?> GetToken(string token);
    Task CreateToken(Guid userId, string refreshToken, DateTime expiryTime);
    Task RevokeTokensByUserId(Guid userId);
}
