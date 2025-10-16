namespace Domain.Tokens;

public interface ITokenRepository
{
    Task<Token?> GetToken(string token);
    Task SaveOrUpdateTokenAsync(Guid userId, string refreshToken, DateTime expiryTime);
}
