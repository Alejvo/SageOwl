namespace Domain.Teams;

public interface ITeamRepository
{
    Task<List<Team>> GetTeamsByUserId(Guid userId);
    Task<Team?> GetTeamById(Guid id);
    Task<bool> CreateTeam(Team team);
    Task<bool> UpdateTeam(Team team);
    Task<bool> DeleteTeam(Team team);
}
