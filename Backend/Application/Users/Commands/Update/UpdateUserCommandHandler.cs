using Application.Abstractions;
using Application.Interfaces;
using Domain.Users;
using Shared;

namespace Application.Users.Commands.Update;

internal sealed class UpdateUserCommandHandler(
    IUserRepository userRepository,
    ICacheService cacheService,
    IUnitOfWork unitOfWork) : ICommandHandler<UpdateUserCommand>
{
    public async Task<Result> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetUserById(request.Id);

        if (user is null)
            return Result.Failure(UserErrors.UserNotFound());

        await cacheService.RemoveAsync($"user:{user.Id}");

        var updatedUser = User.Create(
            request.Id,
            request.Name.Capitalize(),
            request.Surname.Capitalize(),
            request.Email,
            request.Password,
            request.Username.ToLower(),
            request.Birthday);

        await userRepository.Update(user, updatedUser);
        return await unitOfWork.SaveChangesAsync(cancellationToken) ? Result.Success() : Result.Failure(Error.DBFailure);
    }
}
