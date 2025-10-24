using Domain.Forms;
using Domain.Teams;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;
public class FormRepository : IFormRepository
{
    private readonly AppDbContext _dbContext;

    public FormRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> CreateForm(Form form)
    {
        _dbContext.Forms.Add(form);
        return await _dbContext.SaveChangesAsync() > 0;
    }

    public async Task<List<Form>> GetByTeamId(Guid teamId)
    {
        return await _dbContext.Forms
            .Where(f => f.TeamId == teamId)
            .ToListAsync();
    }

    public Task<FormResult> GetFormResults(Guid formId)
    {
        throw new NotImplementedException();
    }

    public async Task<Form?> GetFormById(Guid formId)
    {
        return await _dbContext.Forms
             .Include(f => f.Questions)
                .ThenInclude(f => f.Options)
            .FirstOrDefaultAsync(f => f.Id == formId);
    }

    public async Task<List<Form>> GetPendingFormsByUserId(Guid userId)
    {
        return await _dbContext.Forms
            .Where(f => f.Results.Any(r => r.UserId == userId) && f.Results.Any(r => r.Status == ResultStatus.Pending))
            .ToListAsync();
    }

    public async Task<bool> UpdateForm(Form form)
    {
        if (_dbContext.Entry(form).State == EntityState.Detached)
        {
            _dbContext.Forms.Update(form);
        }
        return await _dbContext.SaveChangesAsync() > 0;
    }
}
