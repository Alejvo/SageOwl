namespace Domain.Tokens;

public interface ITokenRepository
{
    Task<Token?> GetToken(string token);
    Task SaveToken(Guid userId, string token, Guid id, DateTime expiryTime);
}
