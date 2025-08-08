namespace Domain.Teams;

public interface ITeamRepository
{
    Task<List<Team>> GetTeams();
    Task<Team?> GetTeamById(Guid id);
    Task<bool> CreateTeam(Team team);
    Task<bool> UpdateTeam(Team team);
}
