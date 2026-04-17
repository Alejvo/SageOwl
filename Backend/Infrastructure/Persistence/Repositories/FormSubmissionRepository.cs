using Domain.FormSubmissions;
using Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class FormSubmissionRepository : IFormSubmissionRepository
{
    private readonly AppDbContext _dbContext;

    public FormSubmissionRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task CreateFormSubmission(FormSubmission formSubmission)
        => await _dbContext.FormSubmission.AddAsync(formSubmission);

    public async Task<List<FormSubmission>> GetFormSubmissionsByForm(Guid formId)
    {
        return await _dbContext.FormSubmission
            .Where(f => f.FormId == formId)
            .Include(f => f.Answers)
            .ToListAsync();
    }
}
