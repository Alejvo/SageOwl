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
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _mediator = mediator;
    }

    public async Task<Result> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var hashedPassword = _passwordHasher.Hash(request.Password);
        var user = User.Create(Guid.NewGuid(),request.Name,request.Surname,request.Email,hashedPassword,request.Username,request.Birthday);

        foreach (var userEvent in user.DomainEvents)
        {
            if(userEvent is UserCreatedDomainEvent ue)
                await _mediator.Publish(new UserCreatedNotification(ue.Name, ue.Email));
        }

        user.ClearDomainEvents();

        return await _userRepository.CreateUser(user) ? Result.Success() : Result.Failure(Error.DBFailure);
    }
}
