using Domain.Users;

namespace Domain.Tokens;

public class Token
{
    public Guid Id { get; set; }
    public string JwtToken { get; set; }
    public DateTime ExpiryTime { get; set; }
    public Guid UserId { get; set; }
    public User? User { get; set; }

    protected Token() { }
    private Token(string jwtToken, Guid userId)
    {
        Id = Guid.NewGuid();
        JwtToken = jwtToken;
        ExpiryTime = DateTime.Now.AddMinutes(15);
        UserId = userId;
    }

    public static Token Create(string jwtToken, Guid userId) => new(jwtToken, userId);
}
