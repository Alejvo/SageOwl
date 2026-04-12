using Application.Abstractions;
using Application.Teams.Common;

namespace Application.Teams.Queries.GetByUserId;

public record GetTeamsByUserIdQuery(
    Guid UserId
):IQuery<List<TeamResponse>>;
