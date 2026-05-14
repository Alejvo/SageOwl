using Application.Abstractions;
using Domain.Teams;
using Shared;

namespace Application.Teams.Queries.IsAdmin;

internal sealed class IsUserAdminQueryHandler(ITeamRepository teamRepository) : 
    IQueryHandler<IsUserAdminQuery, bool>
{
    private readonly ITeamRepository _teamRepository = teamRepository;

    public async Task<Result<bool>> Handle(IsUserAdminQuery request, CancellationToken cancellationToken)
    {
        return await _teamRepository.IsUserAdmin(request.UserId,request.TeamId);
    }
}
