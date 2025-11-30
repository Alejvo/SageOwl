using Application.Abstractions;
using Application.Users.Events;
using Domain.Users;
using Domain.Users.Events;
using MediatR;
using Shared;

namespace Application.Users.Commands.Create;

internal sealed class CreateUserCommandHandler : ICommandHandler<CreateUserCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IMediator _mediator;

    public CreateUserCommandHandler(IUserRepository userRepository, IPasswordHasher passwordHasher,IMediator mediator)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        _passwordHasher = passwordHasher ?? throw new ArgumentNullException(nameof(passwordHasher));
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
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

        return await _userRepository.CreateUser(user) ? Result.Success() : Result.Failure(Error.DBFailure);
    }
}
