using Application.Abstractions;
using Application.Teams.Common;

namespace Application.Teams.GetAll;

public record GetTeamsQuery(
    
):IQuery<List<TeamResponse>>;
