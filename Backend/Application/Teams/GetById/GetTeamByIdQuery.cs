using Application.Abstractions;
using Application.Teams.Common;

namespace Application.Teams.GetById;

public record GetTeamByIdQuery(Guid TeamId) : IQuery<TeamResponse>;
