using Application.Abstractions;
using Application.Interfaces;
using Domain.Teams;
using Shared;

namespace Application.Teams.Commands.Update;

internal sealed class UpdateTeamCommandHandler : ICommandHandler<UpdateTeamCommand>
{
    private readonly ITeamRepository _teamRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateTeamCommandHandler(ITeamRepository teamRepository, IUnitOfWork unitOfWork)
    {
        _teamRepository = teamRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(UpdateTeamCommand request, CancellationToken cancellationToken)
    {

        var team = await _teamRepository.GetTeamById(request.TeamId);

        if (team == null) return Result.Failure(Error.DBFailure);

        team.Name = request.Name;
        team.Description = request.Description;

        foreach (var member in request.Members)
        {
            team.AddMember(member.UserId, TeamRole.FromString(member.Role));
        }

        team.EnsureAtLeastOneAdmin();

        await _teamRepository.UpdateTeam(team);

        return await _unitOfWork.SaveChangesAsync(cancellationToken) 
            ? Result.Success()
            : Result.Failure(Error.DBFailure);
    }
}
