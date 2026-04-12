using Application.Abstractions;
using Application.Interfaces;
using Domain.Teams;
using Shared;

namespace Application.Teams.Commands.Delete;

internal sealed class DeleteTeamCommandHandler : ICommandHandler<DeleteTeamCommand>
{
    private readonly ITeamRepository _teamRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteTeamCommandHandler(ITeamRepository teamRepository,IUnitOfWork unitOfWork)
    {
        _teamRepository = teamRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(DeleteTeamCommand request, CancellationToken cancellationToken)
    {
        var team = await _teamRepository.GetTeamById(request.TeamId);
        if(team == null) return Result.Failure(Error.DBFailure);

        await _teamRepository.DeleteTeam(team);

        return await _unitOfWork.SaveChangesAsync(cancellationToken)
            ? Result.Success()
            : Result.Failure(Error.DBFailure);
    }
}
