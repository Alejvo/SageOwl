using Domain.Announcements;
using Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class AnnouncementRepository : IAnnouncementRepository
{
    private readonly AppDbContext _dbContext;

    public AnnouncementRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> CreateAnnouncement(Announcement announcement)
    {
        _dbContext.Announcements.Add(announcement);
        return await _dbContext.SaveChangesAsync() > 0;
    }

    public async Task<List<Announcement>> GetAnnouncements()
    {
        return await _dbContext.Announcements
            .Include(a => a.Author)
            .ToListAsync();
    }

    public async Task<List<Announcement>> GetAnnouncementsByTeamId(Guid teamId)
    {
        return await _dbContext.Announcements
            .Where(a => a.TeamId == teamId)
            .Include(a => a.Author)
            .ToListAsync();
    }
}
