namespace Domain.Tokens;

public interface ITokenRepository
{
    Task<Token?> GetToken(string token);
    Task SaveTokenAsync(Guid userId, string refreshToken, DateTime expiryTime);
}
