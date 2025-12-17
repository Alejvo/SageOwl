using Application.Abstractions;
using Application.Interfaces;
using Application.Users.Common;
using Domain.Users;
using Shared;

namespace Application.Users.Queries.GetById;

internal sealed class GetUserByIdQueryHandler(
    IUserRepository userRepository, 
    ICacheService cacheService) : IQueryHandler<GetUserByIdQuery, UserResponse>
{
    public async Task<Result<UserResponse>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var cacheKey = $"user:{request.Id}";

        var cachedUser = await cacheService.GetAsync<UserResponse>(cacheKey);
        if (cachedUser is not null)
            return cachedUser;

        var user = await userRepository.GetUserById(request.Id);

        if (user is null)
            return Result.Failure<UserResponse>(UserErrors.UserNotFound());

        var response = user.ToUserResponse();
        await cacheService.SetAsync(cacheKey, response);

        return response;
    }
}
