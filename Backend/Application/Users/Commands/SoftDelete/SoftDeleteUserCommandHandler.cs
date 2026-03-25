using Application.Abstractions;
using Domain.Users;
using Shared;

namespace Application.Users.Commands.SoftDelete;

internal sealed class SoftDeleteUserCommandHandler : ICommandHandler<SoftDeleteUserCommand>
{
    private readonly IUserRepository _userRepository;

    public SoftDeleteUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Result> Handle(SoftDeleteUserCommand request, CancellationToken cancellationToken)
    {
        return await _userRepository.SoftDelete(request.UserId) 
            ? Result.Success()
            : Result.Failure(UserErrors.UserNotFound());
    }
}
