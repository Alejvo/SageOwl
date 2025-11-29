using Application.Abstractions;
using Application.Users.Common;
using Domain.Users;
using Shared;

namespace Application.Users.Queries.GetAll;

internal sealed class GetUsersQueryHandler : IQueryHandler<GetUsersQuery, PagedList<UserResponse>>
{
    private readonly IUserRepository _userRepository;

    public GetUsersQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Result<PagedList<UserResponse>>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        var usersQuery = await _userRepository.GetUsers();
        var userResponseQuery = usersQuery.Select(user => user.ToUserResponse());

        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            userResponseQuery = userResponseQuery.Where(user =>
                user.Name.StartsWith(request.SearchTerm, StringComparison.OrdinalIgnoreCase));
        }

        Func<UserResponse, object> keySelector = request.SortColumn?.ToLower() switch
        {
            "surname" => user => user.Surname,
            _ => user => user.Name
        };

        userResponseQuery = request.SortOrder == "desc"
            ? userResponseQuery.OrderByDescending(keySelector)
            : userResponseQuery.OrderBy(keySelector);

        userResponseQuery = userResponseQuery.ToList();

        var usersList = await PagedList<UserResponse>.CreateAsync(
                userResponseQuery,
                request.Page,
                request.PageSize
            );

        return usersList;
    }
}
