using Application.Abstractions;
using Application.Teams.Common;

namespace Application.Teams.GetByUserId;

public record GetTeamsByUserIdQuery(
    Guid UserId
):IQuery<List<TeamResponse>>;
