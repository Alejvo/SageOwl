using Domain.Tokens;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace Infrastructure.Persistence.Configurations;

public class TokenConfiguration : IEntityTypeConfiguration<Token>
{
    public void Configure(EntityTypeBuilder<Token> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasIndex(t => t.UserId);

        builder.HasOne(t => t.User)
            .WithMany(u => u.Tokens)
            .HasForeignKey(t => t.UserId)
            .IsRequired(false);

        builder.Property(t => t.RefreshToken)
            .HasColumnName("RefreshToken");

        builder.Property(t => t.RefreshToken).IsRequired();
    }
}
