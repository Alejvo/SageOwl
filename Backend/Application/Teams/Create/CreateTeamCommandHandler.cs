using Application.Abstractions;
using Application.Teams.Common;
using Domain.Teams;
using Domain.Users;
using Shared;

namespace Application.Teams.Create;

internal sealed class CreateTeamCommandHandler : ICommandHandler<CreateTeamCommand>
{
    private readonly ITeamRepository _teamRepository;
    private readonly IUserRepository _userRepository;

    public CreateTeamCommandHandler(ITeamRepository teamRepository,IUserRepository userRepository)
    {
        _teamRepository = teamRepository;
        _userRepository = userRepository;
    }

    public async Task<Result> Handle(CreateTeamCommand request, CancellationToken cancellationToken)
    {
        if (request.Members.Count == 0)
            Result.Failure<TeamResponse>(TeamErrors.NoMembersAdded);

        var team = Team.Create(request.Name,request.Description);

        foreach (var member in request.Members)
        {
            team.AddMember(member.UserId, TeamRole.FromString(member.Role));
        }

        if (!team.Members.Any(m => m.Role == TeamRole.Admin))
            Result.Failure<TeamResponse>(TeamErrors.NoAdminAdded);

        return await _teamRepository.CreateTeam(team) 
            ? Result.Success() 
            : Result.Failure(Error.DBFailure);
    }
}
