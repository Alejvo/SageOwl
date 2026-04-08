using Application.Abstractions;
using Application.Interfaces;
using Domain.Users;
using Shared;

namespace Application.Users.Commands.SoftDelete;

internal sealed class SoftDeleteUserCommandHandler : ICommandHandler<SoftDeleteUserCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public SoftDeleteUserCommandHandler(IUserRepository userRepository,IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(SoftDeleteUserCommand request, CancellationToken cancellationToken)
    {
        await _userRepository.SoftDelete(request.UserId);

        return await _unitOfWork.SaveChangesAsync(cancellationToken)
            ? Result.Success()
            : Result.Failure(UserErrors.UserNotFound());
    }
}
