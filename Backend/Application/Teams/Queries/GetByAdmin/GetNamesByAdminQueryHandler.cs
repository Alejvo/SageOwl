using Application.Abstractions;
using Domain.Teams;
using Shared;

namespace Application.Teams.Queries.GetByAdmin;

internal sealed class GetNamesByAdminQueryHandler : IQueryHandler<GetNamesByAdminQuery,List<string>>
{
    private readonly ITeamRepository _teamRepository;

    public GetNamesByAdminQueryHandler(ITeamRepository teamRepository)
    {
        _teamRepository = teamRepository;
    }

    public async Task<Result<List<string>>> Handle(GetNamesByAdminQuery request, CancellationToken cancellationToken)
    {
        return await _teamRepository.GetTeamNamesByAdmin(request.UserId);
    }
}
