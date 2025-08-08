using Domain.Teams;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class TeamRepository : ITeamRepository
{
    private readonly AppDbContext _dbContext;

    public TeamRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public async Task<bool> CreateTeam(Team team)
    {
        _dbContext.Teams.Add(team);
        return await _dbContext.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteTeam(Team team)
    {
        _dbContext.Teams.Remove(team);
        return await _dbContext.SaveChangesAsync() > 0;
    }

    public async Task<Team?> GetTeamById(Guid id)
    {
        return await _dbContext.Teams
            .Include(t => t.Members)
            .ThenInclude(m => m.User)
            .FirstOrDefaultAsync(team => team.Id == id);
    }

    public async Task<List<Team>> GetTeams()
    {
        return await _dbContext.Teams.ToListAsync();
    }

    public async Task<bool> UpdateTeam(Team team)
    {
        if (_dbContext.Entry(team).State == EntityState.Detached)
        {
            _dbContext.Teams.Update(team);
        }
        return await _dbContext.SaveChangesAsync() > 0;
    }

}
