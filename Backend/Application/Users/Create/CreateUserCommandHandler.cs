using Application.Abstractions;
using Domain.Users;
using Shared;

namespace Application.Users.Create;

internal sealed class CreateUserCommandHandler : ICommandHandler<CreateUserCommand>
{
    private readonly IUserRepository _userRepository;

    public CreateUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Result> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = User.Create(Guid.NewGuid(),request.Name,request.Surname,request.Email,request.Password,request.Username,request.Birthday);
        return await _userRepository.CreateUser(user) ? Result.Success() : Result.Failure(Error.DBFailure);
    }
}
