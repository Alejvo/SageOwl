using Application.Abstractions;
using Domain.Users;
using Shared;

namespace Application.Users.Commands.Update;

internal sealed class UpdateUserCommandHandler : ICommandHandler<UpdateUserCommand>
{
    private readonly IUserRepository _userRepository;

    public UpdateUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    }

    public async Task<Result> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var updatedUser = User.Create(request.Id,request.Name,request.Surname,request.Email,request.Password,request.Username,request.Birthday);
        return await _userRepository.Update(updatedUser) ? Result.Success() : Result.Failure(Error.DBFailure);
    }
}
