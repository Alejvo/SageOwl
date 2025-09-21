using Domain.Qualifications;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class QualificationRepository : IQualificationRepository
{
    private readonly AppDbContext _dbContext;

    public QualificationRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Qualification>> GetQualificationsByTeamId(Guid teamId)
    {
        return await _dbContext.Qualifications
            .Include(q => q.UsersQualifications)
            .Where(q =>  q.TeamId == teamId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Qualification>> GetQualificationsByUserId(Guid userId)
    {
        return await _dbContext.Qualifications
            .Include(q => q.UsersQualifications)
            .Where(q => q.UsersQualifications.Any(uq => uq.UserId == userId))
            .ToListAsync();
    }

    public async Task<bool> SaveQualifications(Qualification qualification)
    {
        var existingQualification = await _dbContext.Qualifications
        .Include(q => q.UsersQualifications)
        .FirstOrDefaultAsync(q => q.Id == qualification.Id);

        if (existingQualification is null)
        {
            await _dbContext.Qualifications.AddAsync(qualification);
        }
        else
        {
            existingQualification.MaximumGrade = qualification.MaximumGrade;
            existingQualification.MinimumGrade = qualification.MinimumGrade;
            existingQualification.PassingGrade = qualification.PassingGrade;
            existingQualification.Period = qualification.Period;

            var toRemove = existingQualification.UsersQualifications
                .Where(eq => !qualification.UsersQualifications.Any(nq => nq.UserId == eq.UserId))
                .ToList();

            _dbContext.UserQualifications.RemoveRange(toRemove);

            foreach (var nq in qualification.UsersQualifications)
            {
                var existing = existingQualification.UsersQualifications
                    .FirstOrDefault(eq => eq.UserId == nq.UserId);

                if (existing is null)
                {
                    existingQualification.AddUserQualification(nq.UserId,nq.Position,nq.Position,nq.HasValue,nq.Description);
                }
                else
                {
                    existing.Update(nq.Grade, nq.Position, nq.HasValue,nq.Description);
                }
            }
        }

        return await _dbContext.SaveChangesAsync() > 0;
    }
}
