using Domain.Forms;
using Domain.Teams;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations.Forms;

public class FormResultConfiguration : IEntityTypeConfiguration<FormResult>
{
    public void Configure(EntityTypeBuilder<FormResult> builder)
    {
        builder.HasKey(r => r.Id);

        builder.HasOne(r => r.User)
            .WithMany()
            .HasForeignKey(r => r.UserId);

        builder.HasOne(r => r.Form)
            .WithMany(f => f.Results)
            .HasForeignKey(r => r.FormId);

        builder.Property(r => r.Status)
            .HasConversion(
                v => v.Value,
                v => ResultStatus.FromString(v)
                );
    }
}
