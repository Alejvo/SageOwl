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

    public async Task<Token?> GetToken(string token)
    {
        return await _dbContext.Tokens
            .FirstOrDefaultAsync(t => t.RefreshToken == token);
    }

    public async Task SaveOrUpdateTokenAsync(Guid userId, string refreshToken, DateTime expiryTime)
    {
        var existingToken = await _dbContext.Tokens
            .FirstOrDefaultAsync(t => t.UserId == userId);

        if (existingToken is null)
        {
            var newToken = Token.Create(refreshToken, userId, expiryTime);
            _dbContext.Tokens.Add(newToken);
        }
        else
        {
            existingToken.RefreshToken = refreshToken;
            existingToken.ExpiryTime = expiryTime;
            _dbContext.Tokens.Update(existingToken);
        }

        await _dbContext.SaveChangesAsync();
    }
}
