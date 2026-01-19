using Domain.Subscriptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations.Subscriptions;

public class SubscriptionMemberConfiguration : IEntityTypeConfiguration<SubscriptionMember>
{
    public void Configure(EntityTypeBuilder<SubscriptionMember> builder)
    {
        builder.HasKey(s => s.Id);

        builder.HasOne(s => s.LinkedUser)
            .WithMany()
            .HasForeignKey(s => s.LinkedUserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(s => s.Subscription)
            .WithMany()
            .HasForeignKey(s => s.SubscriptionId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
