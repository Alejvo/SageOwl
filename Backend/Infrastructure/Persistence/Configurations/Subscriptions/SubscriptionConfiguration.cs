using Domain.Subscriptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations.Subscriptions;

public class SubscriptionConfiguration : IEntityTypeConfiguration<Subscription>
{
    public void Configure(EntityTypeBuilder<Subscription> builder)
    {
        builder.HasKey(s => s.Id);

        builder.ToTable("subscription");

        builder.HasOne(s => s.Plan)
            .WithMany()
            .HasForeignKey(s => s.PlanId);

        builder.HasOne(s => s.Subscriber)
            .WithMany()
            .HasForeignKey(s => s.SubscriberId);

        builder.Property(s => s.StartAt).IsRequired();
        builder.Property(s => s.EndAt).IsRequired();
    }
}
