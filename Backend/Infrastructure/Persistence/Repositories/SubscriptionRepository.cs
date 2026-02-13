using Domain.Subscriptions;
using Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class SubscriptionRepository : ISubscriptionRepository
{
    private readonly AppDbContext _dbContext;

    public SubscriptionRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ICollection<Subscription>> GetSubscriptionsByUserId(Guid UserId)
    {
        return await _dbContext.Subscriptions
            .Where(s => s.SubscriberId == UserId)
            .ToListAsync();
    }

    public async Task<bool> SaveSubscription(Subscription subscription)
    {
        await _dbContext.Subscriptions
            .AddAsync(subscription);
        return await _dbContext.SaveChangesAsync() > 0;
    }
}
