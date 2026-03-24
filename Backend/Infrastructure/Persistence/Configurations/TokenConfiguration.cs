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
            .WithOne(t => t.Token)
            .HasForeignKey<Token>(t => t.UserId)
            .IsRequired();

        builder.Property(t => t.RefreshToken)
            .HasColumnName("RefreshToken");

        builder.Property(t => t.RefreshToken).IsRequired();
    }
}
