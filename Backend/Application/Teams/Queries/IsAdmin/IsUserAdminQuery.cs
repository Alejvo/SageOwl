using Application.Abstractions;

namespace Application.Teams.Queries.IsAdmin;

public record IsUserAdminQuery(
    Guid UserId,
    Guid TeamId
    ) : IQuery<bool>;
