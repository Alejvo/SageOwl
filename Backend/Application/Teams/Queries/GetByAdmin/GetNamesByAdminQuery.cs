using Application.Abstractions;

namespace Application.Teams.Queries.GetByAdmin;

public record GetNamesByAdminQuery(
    Guid UserId
    ):IQuery<List<string>>;
