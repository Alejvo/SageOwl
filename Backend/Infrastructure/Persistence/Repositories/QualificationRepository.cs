using Domain.Qualifications;
using Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class QualificationRepository : IQualificationRepository
{
    private readonly AppDbContext _dbContext;

    public QualificationRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> CreateQualifications(Qualification qualification)
    {
        await _dbContext.Qualifications.AddAsync(qualification);
        return await _dbContext.SaveChangesAsync() > 0;
    }

    public async Task<Qualification?> GetQualificationById(Guid id)
    {
        return await _dbContext.Qualifications
            .Include(q => q.UserQualifications)
            .Where(q => q.Id == id)
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Qualification>> GetQualificationByTeamId(Guid teamId)
    {
        return await _dbContext.Qualifications
            .Include(q => q.Team)
                .ThenInclude(t => t.Members)
                    .ThenInclude(tm => tm.User)
            .Include(q => q.UserQualifications
                .OrderBy(uq => uq.Position))
            .Where(q => q.TeamId == teamId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Qualification>> GetQualificationsByUserId(Guid userId)
    {
        return await _dbContext.Qualifications
            .Include(q => q.Team)
                .ThenInclude(t => t.Members)
                    .ThenInclude(tm => tm.User)
            .Include(q => q.UserQualifications
                .Where(uq => uq.UserId == userId)
                .OrderBy(uq => uq.Position))
            .ToListAsync();
    }

    public async Task<bool> UpdateQualifications(Qualification qualification)
    {
        if (_dbContext.Entry(qualification).State == EntityState.Detached)
        {
            _dbContext.Qualifications.Update(qualification);
        }
        return await _dbContext.SaveChangesAsync() > 0;
    }
}
