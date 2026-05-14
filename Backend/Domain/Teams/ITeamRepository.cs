namespace Domain.Teams;

public interface ITeamRepository
{
    Task<List<Team>> GetTeamsByUserId(Guid userId);
    Task<Team?> GetTeamById(Guid id);
    Task CreateTeam(Team team);
    Task UpdateTeam(Team team);
    Task DeleteTeam(Team team);
    Task<Dictionary<Guid,string>> GetTeamNamesByAdmin(Guid userId);
    Task<bool> IsUserAdmin(Guid userId, Guid teamId);

}
