using Domain.Subscriptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations.Subscriptions;

public class PlanConfiguration : IEntityTypeConfiguration<Plan>
{
    public void Configure(EntityTypeBuilder<Plan> builder)
    {
        builder.HasKey(x => x.Id);

        builder.ToTable("plan");

        builder.Property(p => p.Value).IsRequired();

        builder.Property(p => p.Name).IsRequired();
    }
}
