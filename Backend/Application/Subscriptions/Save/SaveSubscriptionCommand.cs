using Application.Abstractions;

namespace Application.Subscriptions.Save;

public sealed record SaveSubscriptionCommand(
    Guid SubscriberId,
    int PlanId,
    DateTime StartAt, 
    DateTime EndAt
    ) : ICommand;
