﻿using Domain.Forms;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.Forms;

public class QuestionConfiguration : IEntityTypeConfiguration<Question>
{
    public void Configure(EntityTypeBuilder<Question> builder)
    {
        builder.HasKey(f => f.Id);

        builder.Property(f => f.Title).IsRequired();

        builder.Property(b => b.QuestionType)
            .HasConversion(
                b => b.Value,
                b => QuestionType.FromString(b)
            );

        builder.HasOne(f => f.Form)
            .WithMany(f => f.Questions)
            .HasForeignKey(f => f.FormId);

    }
}
