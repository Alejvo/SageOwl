using Domain.Teams;
using Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class TeamRepository : ITeamRepository
{
    private readonly AppDbContext _dbContext;

    public TeamRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public async Task CreateTeam(Team team) 
        => await _dbContext.Teams.AddAsync(team);

    public async Task DeleteTeam(Team team) 
        => _dbContext.Teams.Remove(team);

    public async Task<Team?> GetTeamById(Guid id)
    {
        return await _dbContext.Teams
            .Include(t => t.Members)
            .ThenInclude(m => m.User)
            .Include(t => t.Forms)
            .Include(t => t.Announcements)
            .FirstOrDefaultAsync(team => team.Id == id);
    }

    public async Task<Dictionary<Guid,string>> GetTeamNamesByAdmin(Guid userId)
    {
        return await _dbContext.Teams
            .Where(t => t.Members.Any(m =>
                m.UserId == userId &&
                m.Role == TeamRole.Admin))
            .Select(t => new { t.Id, t.Name})
            .ToDictionaryAsync(x => x.Id, x => x.Name);
    }

    public async Task<List<Team>> GetTeamsByUserId(Guid userId)
    {
        return await _dbContext.Teams
            .Where(t =>t.Members.Any(m => m.UserId == userId))
            .ToListAsync();
    }

    public async Task<bool> IsUserAdmin(Guid userId, Guid teamId)
    {
        var isAdmin = await _dbContext.TeamMembership
            .Where(tm => tm.UserId == userId && tm.TeamId == teamId)
            .Select(tm => tm.Role == TeamRole.Admin)
            .FirstOrDefaultAsync();

        return isAdmin;
    }

    public async Task UpdateTeam(Team team)
    {
        if (_dbContext.Entry(team).State == EntityState.Detached)
        {
            _dbContext.Teams.Update(team);
        }
    }

}
