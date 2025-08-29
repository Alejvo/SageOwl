using Domain.Announcements;
using Domain.Forms;
using Domain.Teams;
using Domain.Tokens;
using Domain.Users;
using Infrastructure.Configurations;
using Infrastructure.Configurations.Announcements;
using Infrastructure.Configurations.Forms;
using Infrastructure.Configurations.Teams;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Contexts;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }
    //Users
    public DbSet<User> Users { get; set; }
    public DbSet<Token> Tokens { get; set; }
    //Teams
    public DbSet<Team> Teams { get; set; }
    public DbSet<TeamMembership> TeamMembership { get; set; }
    //Forms
    public DbSet<Form> Forms { get; set; }
    public DbSet<Question> Questions { get; set; }
    public DbSet<Option> Options { get; set; }
    public DbSet<Answer> Answers { get; set; }
    public DbSet<FormResult> FormResults { get; set; }
    //Announcements
    public DbSet<Announcement> Announcements { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        /*
        modelBuilder.ApplyConfiguration(new AnswerConfiguration());
        modelBuilder.ApplyConfiguration(new FormConfiguration());
        modelBuilder.ApplyConfiguration(new OptionConfiguration());
        modelBuilder.ApplyConfiguration(new QuestionConfiguration());
        modelBuilder.ApplyConfiguration(new TeamConfiguration());
        modelBuilder.ApplyConfiguration(new TeamMembershipConfiguration());
        modelBuilder.ApplyConfiguration(new TokenConfiguration());
        modelBuilder.ApplyConfiguration(new UserConfiguration());*/
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AnnouncementConfiguration).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
