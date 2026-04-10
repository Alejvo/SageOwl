using Application.Abstractions;
using Application.Interfaces;
using Domain.Subscriptions;
using Shared;

namespace Application.Subscriptions.Save;

internal sealed class SaveSubscriptionCommandHandler : ICommandHandler<SaveSubscriptionCommand>
{
    private readonly ISubscriptionRepository _subscriptionRepository;
    private readonly IUnitOfWork _unitOfWork;

    public SaveSubscriptionCommandHandler(ISubscriptionRepository subscriptionRepository,IUnitOfWork unitOfWork)
    {
        _subscriptionRepository = subscriptionRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(SaveSubscriptionCommand request, CancellationToken cancellationToken)
    {
        var subscription = Subscription.Create(
            request.SubscriberId,
            request.PlanId,
            request.StartAt,
            request.EndAt);

        await _subscriptionRepository.SaveSubscription(subscription);

        return await _unitOfWork.SaveChangesAsync(cancellationToken)
            ? Result.Success()
            : Result.Failure(Error.DBFailure);
    }
}
