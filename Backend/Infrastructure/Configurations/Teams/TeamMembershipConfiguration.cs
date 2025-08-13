using Domain.Teams;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.Teams;

public class TeamMembershipConfiguration : IEntityTypeConfiguration<TeamMembership>
{
    public void Configure(EntityTypeBuilder<TeamMembership> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(t => t.Id)
            .ValueGeneratedOnAdd();

        builder.HasOne(t => t.User)
            .WithMany(u => u.UserTeams)
            .HasForeignKey(t => t.UserId);

        builder.HasOne(t => t.Team)
            .WithMany(t => t.Members)
            .HasForeignKey(t => t.TeamId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(t => t.Role)
            .HasConversion(
                v => v.Value,
                v => TeamRole.FromString(v)
            )
            .HasColumnName("Role")
            .IsRequired();

    }
}
