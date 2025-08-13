using Domain.Forms;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.Forms;

public class FormConfiguration : IEntityTypeConfiguration<Form>
{
    public void Configure(EntityTypeBuilder<Form> builder)
    {
        builder.HasKey(f => f.Id);

        builder.Property(f => f.Title).IsRequired();

        builder.HasOne(f => f.Team)
            .WithMany(t => t.Forms)
            .HasForeignKey(t => t.TeamId);
    }
}
