using Application.Abstractions;
using Application.Interfaces;
using Domain.Tokens;
using Domain.Users;
using Shared;

namespace Application.Users.Commands.SoftDelete;

internal sealed class SoftDeleteUserCommandHandler : ICommandHandler<SoftDeleteUserCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITokenRepository _tokenRepository;

    public SoftDeleteUserCommandHandler(
        IUserRepository userRepository,
        IUnitOfWork unitOfWork,
        ITokenRepository tokenRepository)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _tokenRepository = tokenRepository;
    }

    public async Task<Result> Handle(SoftDeleteUserCommand request, CancellationToken cancellationToken)
    {
        await _userRepository.SoftDelete(request.UserId);
        await _tokenRepository.RevokeTokensByUserId(request.UserId);

        return await _unitOfWork.SaveChangesAsync(cancellationToken)
            ? Result.Success()
            : Result.Failure(UserErrors.UserNotFound());
    }
}
