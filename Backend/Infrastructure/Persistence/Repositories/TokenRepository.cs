using Domain.Tokens;
using Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

internal class TokenRepository : ITokenRepository
{
    private readonly AppDbContext _dbContext;

    public TokenRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task RevokeTokensByUserId(Guid userId)
    {
        await _dbContext.Tokens
            .Where(t => t.UserId == userId)
            .ExecuteUpdateAsync(setters => setters
                .SetProperty(t => t.IsRevoked,true));
    }

    public async Task<Token?> GetToken(string token)
    {
        return await _dbContext.Tokens
            .FirstOrDefaultAsync(t => t.RefreshToken == token);
    }

    public async Task CreateToken(Guid userId, string refreshToken, DateTime expiryTime)
    {
        var newToken = Token.Create(refreshToken, userId, expiryTime);
        await _dbContext.Tokens.AddAsync(newToken);
    }
}
