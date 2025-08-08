using Application.Abstractions;
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
        var team = Team.Create(request.Name,request.Description);

        foreach (var member in request.Members)
        {
            team.AddMember(member.UserId, TeamRole.FromString(member.Role));
        }

        team.EnsureAtLeastOneAdmin();

        return await _teamRepository.CreateTeam(team) 
            ? Result.Success() 
            : Result.Failure(Error.DBFailure);
    }
}
