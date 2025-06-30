using Application.Abstractions;
using Domain.Users;
using Shared;

namespace Application.Users.Create;

internal sealed class CreateUserCommandHandler : ICommandHandler<CreateUserCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;

    public CreateUserCommandHandler(IUserRepository userRepository, IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task<Result> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var hashedPassword = _passwordHasher.Hash(request.Password);
        var user = User.Create(Guid.NewGuid(),request.Name,request.Surname,request.Email,hashedPassword,request.Username,request.Birthday);
        return await _userRepository.CreateUser(user) ? Result.Success() : Result.Failure(Error.DBFailure);
    }
}
