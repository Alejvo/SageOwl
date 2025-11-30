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

        var user = await _userRepository.GetUserById(request.Id);

        if (user is null)
            return Result.Failure(UserErrors.UserNotFound());

        var updatedUser = User.Create(
            request.Id,
            request.Name.Capitalize(),
            request.Surname.Capitalize(),
            request.Email,
            request.Password,
            request.Username.ToLower(),
            request.Birthday);

        return await _userRepository.Update(user,updatedUser) ? Result.Success() : Result.Failure(Error.DBFailure);
    }
}
