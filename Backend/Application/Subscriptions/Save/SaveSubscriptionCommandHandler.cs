using Application.Abstractions;
using Domain.Subscriptions;
using Shared;

namespace Application.Subscriptions.Save;

internal sealed class SaveSubscriptionCommandHandler : ICommandHandler<SaveSubscriptionCommand>
{
    private readonly ISubscriptionRepository _subscriptionRepository;

    public SaveSubscriptionCommandHandler(ISubscriptionRepository subscriptionRepository)
    {
        _subscriptionRepository = subscriptionRepository;
    }

    public async Task<Result> Handle(SaveSubscriptionCommand request, CancellationToken cancellationToken)
    {
        var subscription = Subscription.Create(
            request.SubscriberId,
            request.PlanId,
            request.StartAt,
            request.EndAt);

        return await _subscriptionRepository.SaveSubscription(subscription) 
            ? Result.Success()
            : Result.Failure(Error.DBFailure);
    }
}
