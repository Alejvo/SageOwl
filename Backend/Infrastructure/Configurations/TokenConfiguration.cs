using Domain.Tokens;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

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

        builder.Property(t => t.JwtToken).IsRequired();
    }
}
