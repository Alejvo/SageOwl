using Application.Abstractions;
using Domain.Teams;
using Domain.Users;
using Shared;

namespace Application.Teams.Update;

internal sealed class UpdateTeamCommandHandler : ICommandHandler<UpdateTeamCommand>
{
    private readonly ITeamRepository _teamRepository;

    public UpdateTeamCommandHandler(ITeamRepository teamRepository)
    {
        _teamRepository = teamRepository;
    }

    public async Task<Result> Handle(UpdateTeamCommand request, CancellationToken cancellationToken)
    {

        var team = await _teamRepository.GetTeamById(request.TeamId);

        if (team == null) return Result.Failure(Error.DBFailure);

        team.Name = request.Name;
        team.Description = request.Description;

        var userIds = request.Members.Select(m => m.UserId).ToList();

        foreach (var member in request.Members)
        {
            team.AddMember(member.UserId, TeamRole.FromString(member.Role));
        }

        team.EnsureAtLeastOneAdmin();

        return await _teamRepository.UpdateTeam(team)
            ? Result.Success()
            : Result.Failure(Error.DBFailure);
    }
}
