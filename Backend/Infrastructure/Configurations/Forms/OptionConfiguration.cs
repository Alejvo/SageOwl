using Domain.Forms;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.Forms;

public class OptionConfiguration : IEntityTypeConfiguration<Option>
{
    public void Configure(EntityTypeBuilder<Option> builder)
    {
        builder.HasKey(o => o.Id);

        builder.Property(o => o.Value)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(o => o.IsCorrect)
            .IsRequired();

        builder.HasOne(o => o.Question)
            .WithMany(q => q.Options)
            .HasForeignKey(o => o.QuestionId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(o => o.Id)
            .ValueGeneratedOnAdd();
    }
}
