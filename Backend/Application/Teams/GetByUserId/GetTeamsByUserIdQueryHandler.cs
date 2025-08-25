using Application.Abstractions;
using Application.Teams.Common;
using Domain.Teams;
using Domain.Users;
using Shared;

namespace Application.Teams.GetByUserId;

public sealed class GetTeamsByUserIdQueryHandler : IQueryHandler<GetTeamsByUserIdQuery, List<TeamResponse>>
{
    private readonly ITeamRepository _teamRepository;
    public GetTeamsByUserIdQueryHandler(ITeamRepository teamRepository)
    {
        _teamRepository = teamRepository;
    }

    public async Task<Result<List<TeamResponse>>> Handle(GetTeamsByUserIdQuery request, CancellationToken cancellationToken)
    {
        var teams = await _teamRepository.GetTeamsByUserId(request.UserId);
        return teams.Select(t => t.ToTeamResponse()).ToList();
    }
}
