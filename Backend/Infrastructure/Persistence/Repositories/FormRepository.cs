using Domain.Forms;
using Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;
public class FormRepository : IFormRepository
{
    private readonly AppDbContext _dbContext;

    public FormRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task CreateForm(Form form)
        => await _dbContext.Forms.AddAsync(form);

    public async Task<List<Form>> GetByTeamId(Guid teamId)
    {
        return await _dbContext.Forms
            .Where(f => f.TeamId == teamId)
            .ToListAsync();
    }

    public async Task<Form?> GetFormById(Guid formId)
    {
        return await _dbContext.Forms
                .Include(f => f.Questions)
                    .ThenInclude(q => (q as ClosedQuestion).Options)
                .FirstOrDefaultAsync(f => f.Id == formId);
    }

    public async Task DeleteForm(Form form) 
        => _dbContext.Forms.Remove(form);
}
