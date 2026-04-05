using Domain.Teams;
using Domain.Users;
using Infrastructure.Persistence.Contexts;
using Microsoft.Extensions.DependencyInjection;

namespace SageOwl.IntegrationTests;

public static class TestDataSeeder
{
    public static async Task SeedAsync(IServiceProvider services)
    {
        using var scope = services.CreateScope();

        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        var passwordHasher = scope.ServiceProvider.GetRequiredService<IPasswordHasher>();

        await db.Database.EnsureCreatedAsync();

        if (db.Users.Any()) return;

        var user = User.Create(
            Guid.NewGuid(),
            "John",
            "Doe",
            "user@test.com",
            passwordHasher.Hash("123456"),
            "John123",
            new DateTime(1999, 01, 23)
            );


        var TeamA = Guid.NewGuid();
        var TeamB = Guid.NewGuid();

        var teams = new List<Team>
        {
            new Team {
                Id = TeamA,
                Name = "Team A",
                Description ="Generic Description"
                },
            new Team { Id = TeamB, Name = "Team B", Description ="Another Generic Description"}
        };

        var teamMemberships = new List<TeamMembership>{
            TeamMembership.Create(user.Id, TeamA, TeamRole.Admin),
            TeamMembership.Create(user.Id, TeamB, TeamRole.Admin)
        };


        db.Users.Add(user);
        db.Teams.AddRange(teams);
        db.AddRange(teamMemberships);

        await db.SaveChangesAsync();
    } 
}
