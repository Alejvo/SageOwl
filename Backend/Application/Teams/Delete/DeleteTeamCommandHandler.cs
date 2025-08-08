using Application.Abstractions;
using Domain.Teams;
using Shared;

namespace Application.Teams.Delete;

internal sealed class DeleteTeamCommandHandler : ICommandHandler<DeleteTeamCommand>
{
    private readonly ITeamRepository _teamRepository;

    public DeleteTeamCommandHandler(ITeamRepository teamRepository)
    {
        _teamRepository = teamRepository;
    }

    public async Task<Result> Handle(DeleteTeamCommand request, CancellationToken cancellationToken)
    {
        var team = await _teamRepository.GetTeamById(request.TeamId);
        if(team == null) return Result.Failure(Error.DBFailure);

        return await _teamRepository.DeleteTeam(team) 
            ? Result.Success()
            : Result.Failure(Error.DBFailure);
    }
}
