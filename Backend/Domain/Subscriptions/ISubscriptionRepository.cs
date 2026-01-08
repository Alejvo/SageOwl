namespace Domain.Subscriptions;

public interface ISubscriptionRepository
{
    Task<Subscription> GetSubscriptionsByUserId(Guid UserId);
    Task SaveSubscription(Subscription subscription);
}
