using Domain.Tokens;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

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
            .FirstOrDefaultAsync(t => t.JwtToken == token);
    }

    public async Task SaveToken(Guid userId, string token, Guid id, DateTime expiryTime)
    {
        var existingToken = await _dbContext.Tokens
                .FirstOrDefaultAsync(t => t.UserId == userId);

        if (existingToken == null)
        {
            var newToken = Token.Create(token,userId);

            _dbContext.Tokens.Add(newToken);
        }
        else
        {
            existingToken.JwtToken = token;
            existingToken.ExpiryTime = expiryTime;

            _dbContext.Tokens.Update(existingToken);
        }

        await _dbContext.SaveChangesAsync();
    }
}
