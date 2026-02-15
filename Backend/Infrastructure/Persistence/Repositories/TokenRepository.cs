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

    public async Task SaveTokenAsync(Guid userId, string refreshToken, DateTime expiryTime)
    {
        var isTokenAvailable = await _dbContext.Tokens.AnyAsync(t => t.RefreshToken == refreshToken);

        if (isTokenAvailable)
        {
            var newToken = Token.Create(refreshToken, userId, expiryTime);
            _dbContext.Tokens.Add(newToken);
            await _dbContext.SaveChangesAsync();
        }

    }
}
