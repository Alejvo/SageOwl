﻿using Application.Abstractions;
using Application.Users.Common;
using Shared;

namespace Application.Users.GetAll;

public record GetUsersQuery(
        string? SearchTerm,
        string? SortColumn,
        string? SortOrder,
        int Page,
        int PageSize
    ):IQuery<PagedList<UserResponse>>;
