using Domain.Announcements;
using Domain.Forms;
using Domain.FormSubmissions;
using Domain.Qualifications;
using Domain.Subscriptions;
using Domain.Teams;
using Domain.Tokens;
using Domain.Users;
using Infrastructure.Persistence.Configurations.Tokens;
using Infrastructure.Persistence.Configurations.Announcements;
using Infrastructure.Persistence.Configurations.Forms;
using Infrastructure.Persistence.Configurations.FormSubmissions;
using Infrastructure.Persistence.Configurations.Qualifications;
using Infrastructure.Persistence.Configurations.Subscriptions;
using Infrastructure.Persistence.Configurations.Teams;
using Infrastructure.Persistence.Configurations.Users;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Contexts;

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
    // FormSubmissions
    public DbSet<Answer> Answers { get; set; }
    public DbSet<FormSubmission> FormSubmission { get; set; }
    //Announcements
    public DbSet<Announcement> Announcements { get; set; }
    //Qualifications
    public DbSet<Qualification> Qualifications { get; set; }
    public DbSet<UserQualification> UserQualifications { get; set; }
    //Subscriptions
    public DbSet<Plan> Plans { get; set; }
    public DbSet<SubscriptionMember> SubscriptionMembers { get; set; }
    public DbSet<Subscription> Subscriptions { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new AnnouncementConfiguration());
        modelBuilder.ApplyConfiguration(new AnswerConfiguration());
        modelBuilder.ApplyConfiguration(new FormConfiguration());
        modelBuilder.ApplyConfiguration(new FormSubmissionConfiguration());
        modelBuilder.ApplyConfiguration(new OptionConfiguration());
        modelBuilder.ApplyConfiguration(new QuestionConfiguration());
        modelBuilder.ApplyConfiguration(new TeamConfiguration());
        modelBuilder.ApplyConfiguration(new TeamMembershipConfiguration());
        modelBuilder.ApplyConfiguration(new TokenConfiguration());
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new QualificationConfiguration());
        modelBuilder.ApplyConfiguration(new UserQualificationConfiguration());
        modelBuilder.ApplyConfiguration(new PlanConfiguration());
        modelBuilder.ApplyConfiguration(new SubscriptionMemberConfiguration());
        modelBuilder.ApplyConfiguration(new SubscriptionConfiguration());

        // Query Filters
        modelBuilder.Entity<User>().HasQueryFilter(u => !u.IsDeleted);

        base.OnModelCreating(modelBuilder);
    }
}
