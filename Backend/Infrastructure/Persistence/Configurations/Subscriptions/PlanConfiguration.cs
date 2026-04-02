using Domain.Subscriptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations.Subscriptions;

public class PlanConfiguration : IEntityTypeConfiguration<Plan>
{
    public void Configure(EntityTypeBuilder<Plan> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(p => p.Id)
       .ValueGeneratedOnAdd();

        builder.Property(p => p.Value).IsRequired();

        builder.Property(p => p.Name).IsRequired();

        builder.HasData(
            new Plan
            {
                Id = 1,
                Name = "Free",
                Value = 0
            },
            new Plan
            {
                Id = 2,
                Name = "Basic",
                Value = 20
            }
        );
    }
}
