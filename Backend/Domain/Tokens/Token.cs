using Domain.Users;

namespace Domain.Tokens;

public class Token
{
    public Guid Id { get; set; }
    public string RefreshToken { get; set; }
    public DateTime ExpiryTime { get; set; }
    public Guid UserId { get; set; }
    public User? User { get; set; }

    protected Token() { }
    private Token(string refreshToken, Guid userId,DateTime expiryTime)
    {
        Id = Guid.NewGuid();
        RefreshToken = refreshToken;
        ExpiryTime = expiryTime;
        UserId = userId;
    }

    public static Token Create(string refreshToken, Guid userId, DateTime expiryTime) => new(refreshToken, userId,expiryTime);
}
