using Domain.Users;

namespace Domain.Subscriptions;

public class Subscription
{
    public Guid Id { get; set; }
    public Guid SubscriberId { get; set; }
    public Guid PlanId { get; set; }
    public DateTime StartAt { get; set; }
    public DateTime EndAt { get; set; }

    public User Subscriber { get; set; }
    public Plan Plan { get; set; }

    private Subscription(DateTime startAt, DateTime endAt)
    {
        StartAt = startAt;
        EndAt = endAt;
    }

    public Subscription() { }

    public  static Subscription Create(DateTime startAt, DateTime endAt)
        => new (startAt, endAt);       
}
