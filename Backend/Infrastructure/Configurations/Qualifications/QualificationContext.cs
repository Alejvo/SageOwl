using Domain.Qualifications;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.Qualifications;

public class QualificationContext : IEntityTypeConfiguration<Qualification>
{
    public void Configure(EntityTypeBuilder<Qualification> builder)
    {
        builder.HasKey(q => q.Id);

        builder.HasOne(q => q.Team)
            .WithMany()
            .HasForeignKey(q => q.TeamId);

        builder.Property(q => q.MaximumGrade).IsRequired();
        builder.Property(q => q.MinimumGrade).IsRequired();
        builder.Property(q => q.PassingGrade).IsRequired();
        builder.Property(q => q.Period).IsRequired();
        builder.Property(q => q.TotalGrades).IsRequired();
    }
}
