using Application.Abstractions;
using Application.Interfaces;
using Application.Users.Events;
using Domain.Subscriptions;
using Domain.Users;
using Domain.Users.Events;
using MediatR;
using Shared;

namespace Application.Users.Commands.Create;

public sealed class CreateUserCommandHandler : ICommandHandler<CreateUserCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly ISubscriptionRepository _subscriptionRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IMediator _mediator;
    private readonly IUnitOfWork _unitOfWork;

    public CreateUserCommandHandler(
        IUserRepository userRepository, 
        IPasswordHasher passwordHasher,
        IMediator mediator, 
        ISubscriptionRepository subscriptionRepository,
        IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        _passwordHasher = passwordHasher ?? throw new ArgumentNullException(nameof(passwordHasher));
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _subscriptionRepository = subscriptionRepository ?? throw new ArgumentNullException(nameof(subscriptionRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<Result> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        if (await _userRepository.EmailExists(request.Email)) 
            return Result.Failure(UserErrors.EmailAlreadyExists(request.Email));

        if (await _userRepository.UsernameExists(request.Username))
            return Result.Failure(UserErrors.UsernameAlreadyExists(request.Username));

        var hashedPassword = _passwordHasher.Hash(request.Password);
        var user = User.Create(
            Guid.NewGuid(),
            request.Name.Capitalize(),
            request.Surname.Capitalize(),
            request.Email,
            hashedPassword,
            request.Username.ToLower(),
            request.Birthday);

        foreach (var userEvent in user.DomainEvents)
        {
            if(userEvent is UserCreatedDomainEvent ue)
                await _mediator.Publish(new UserCreatedNotification(ue.Name, ue.Email),cancellationToken);
        }

        user.ClearDomainEvents();

        var newSubscription = Subscription.Create(
            user.Id,
            1,
            DateTime.UtcNow,
            DateTime.UtcNow.AddYears(1));

        await _userRepository.CreateUser(user);
        await _subscriptionRepository.SaveSubscription(newSubscription);

        var result = await _unitOfWork.SaveChangesAsync(cancellationToken);

        return result ? Result.Success() : Result.Failure(Error.DBFailure);
    }
}
