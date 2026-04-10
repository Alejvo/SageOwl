namespace Domain.Subscriptions;

public interface ISubscriptionRepository
{
    Task<ICollection<Subscription>> GetSubscriptionsByUserId(Guid UserId);
    Task SaveSubscription(Subscription subscription);
}
