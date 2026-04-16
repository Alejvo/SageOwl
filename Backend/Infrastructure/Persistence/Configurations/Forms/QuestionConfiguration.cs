using Domain.Forms;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations.Forms;

public class QuestionConfiguration : IEntityTypeConfiguration<Question>
{
    public void Configure(EntityTypeBuilder<Question> builder)
    {
        builder.ToTable("Questions");

        builder.HasKey(f => f.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();

        builder.Property(q => q.Text)
            .IsRequired()
            .HasMaxLength(500);

        builder.HasDiscriminator<string>("QuestionType")
            .HasValue<OpenQuestion>("Open")
            .HasValue<ClosedQuestion>("Closed");
    }
}
