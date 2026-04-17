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

    public async Task CreateQualification(Qualification qualification) 
        => await _dbContext.Qualifications.AddAsync(qualification);

    public void DeleteQualification(Qualification qualification)
        => _dbContext.Qualifications.Remove(qualification);

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
             .Where(q => q.TeamId == teamId)
            .Include(q => q.Team)
                .ThenInclude(t => t.Members)
                    .ThenInclude(tm => tm.User)
            .Include(q => q.UserQualifications)
            .ToListAsync();
    }

    public async Task<IEnumerable<Qualification>> GetQualificationsByUserId(Guid userId)
    {
        return await _dbContext.Qualifications
            .Include(q => q.Team)
                .ThenInclude(t => t.Members)
                    .ThenInclude(tm => tm.User)
            .Include(q => q.UserQualifications
                .Where(uq => uq.UserId == userId))
            .ToListAsync();
    }
}
