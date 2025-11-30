using Application.Abstractions;
using Application.Teams.Common;
using Domain.Teams;
using Domain.Users;
using Shared;

namespace Application.Teams.GetById;

internal sealed class GetTeamByIdQueryHandler : IQueryHandler<GetTeamByIdQuery, TeamResponse>
{
    private readonly ITeamRepository _teamRepository;

    public GetTeamByIdQueryHandler(ITeamRepository teamRepository)
    {
        _teamRepository = teamRepository;
    }

    public async Task<Result<TeamResponse>> Handle(GetTeamByIdQuery request, CancellationToken cancellationToken)
    {
        var team = await _teamRepository.GetTeamById(request.TeamId);
        return team is not null ? team.ToTeamResponse() : Result.Failure<TeamResponse>(TeamErrors.NotFound);
    }
}
