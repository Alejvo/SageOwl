using Domain.Forms;
using Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.Forms;

public class AnswerConfiguration : IEntityTypeConfiguration<Answer>
{
    public void Configure(EntityTypeBuilder<Answer> builder)
    {
        builder.HasKey(a => a.Id);

        builder.HasOne<Question>()
            .WithMany()
            .HasForeignKey(a => a.QuestionId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<Option>()
            .WithMany()
            .HasForeignKey(a => a.SelectedOptionId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(a => a.OpenQuestionAnswer)
            .IsRequired(false);
    }
}
