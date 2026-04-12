using Application.Abstractions;
using Application.Teams.Common;

namespace Application.Teams.Queries.GetById;

public record GetTeamByIdQuery(Guid TeamId) : IQuery<TeamResponse>;
