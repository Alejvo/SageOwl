using Application.Abstractions;
using Application.Users.Common;

namespace Application.Users.GetById;

public record GetUserByIdQuery(
        Guid Id
    ): IQuery<UserResponse>;
