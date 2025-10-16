using Domain.Qualifications;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.Qualifications;

public class UserQualificationConfiguration : IEntityTypeConfiguration<UserQualification>
{
    public void Configure(EntityTypeBuilder<UserQualification> builder)
    {
        builder.HasKey(uq => uq.Id);

        builder.HasOne(uq => uq.Qualification)
            .WithMany(q => q.UserQualifications)
            .HasForeignKey(uq => uq.QualificationId);

        builder.HasOne(uq => uq.User)
            .WithMany()
            .HasForeignKey(uq => uq.UserId);

        builder.Property(uq => uq.Grade).IsRequired();
        builder.Property(uq => uq.Description).IsRequired(false);
        builder.Property(uq => uq.Position).IsRequired();
        builder.Property(uq => uq.HasValue).IsRequired();
    }
}
