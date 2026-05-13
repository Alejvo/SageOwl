using Application.Abstractions;
using Domain.Teams;
using Shared;

namespace Application.Teams.Queries.GetByAdmin;

internal sealed class GetNamesByAdminQueryHandler : IQueryHandler<GetNamesByAdminQuery,Dictionary<Guid,string>>
{
    private readonly ITeamRepository _teamRepository;

    public GetNamesByAdminQueryHandler(ITeamRepository teamRepository)
    {
        _teamRepository = teamRepository;
    }

    public async Task<Result<Dictionary<Guid, string>>> Handle(GetNamesByAdminQuery request, CancellationToken cancellationToken)
    {
        return await _teamRepository.GetTeamNamesByAdmin(request.UserId);
    }
}
