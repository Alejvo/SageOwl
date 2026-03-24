using Domain.Users;

namespace Domain.Subscriptions;

public class SubscriptionMember
{
    public Guid Id { get; set; }
    public Guid LinkedUserId { get; set; }
    public Guid SubscriptionId { get; set; }

    public User LinkedUser { get; set; }
    public Subscription Subscription { get; set; }

    public SubscriptionMember(Guid subscriptionId, Guid linkedUserId)
    {
        SubscriptionId = subscriptionId;
        LinkedUserId = linkedUserId;
    }

    public SubscriptionMember() { }

    public static SubscriptionMember Create(Guid subscriptionId, Guid linkedUserId)
        => new(subscriptionId, linkedUserId);

}
