using Application.Abstractions;
using Application.Users.Common;
using Domain.Users;
using Shared;

namespace Application.Users.Queries.GetById;

internal sealed class GetUserByIdQueryHandler : IQueryHandler<GetUserByIdQuery, UserResponse>
{
    private readonly IUserRepository _userRepository;

    public GetUserByIdQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Result<UserResponse>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserById(request.Id);

        return user != null ? user.ToUserResponse() : Result.Failure<UserResponse>(UserErrors.UserNotFound());
    }
}
