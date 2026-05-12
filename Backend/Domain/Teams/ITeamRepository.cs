namespace Domain.Teams;

public interface ITeamRepository
{
    Task<List<Team>> GetTeamsByUserId(Guid userId);
    Task<Team?> GetTeamById(Guid id);
    Task CreateTeam(Team team);
    Task UpdateTeam(Team team);
    Task DeleteTeam(Team team);
    Task<List<string>> GetTeamNamesByAdmin(Guid userId);
    Task<string?> GetUserRoleInTeam(Guid userId, Guid teamId);

}
