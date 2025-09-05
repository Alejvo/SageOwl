using Application.Abstractions;
using Application.Users.Common;
using Shared;

namespace Application.Users.GetAll;

public record GetUsersQuery(
        int Page,
        int PageSize,
        string? SearchTerm,
        string? SortColumn,
        string? SortOrder
    ):IQuery<PagedList<UserResponse>>;
