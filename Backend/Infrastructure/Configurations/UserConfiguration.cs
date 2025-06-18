using Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");
        builder.HasKey(u => u.Id);

        builder.Property(u => u.Email).HasConversion(
            email => email.Value,
            value => Email.Create(value)
        ).HasMaxLength(70);

        builder.Property(u => u.Password).HasConversion(
            pass => pass.Value,
            value => Password.Create(value)
        ).HasMaxLength(20);

        builder.Property(u => u.Name).HasMaxLength(50);

        builder.Property(u => u.Username).HasMaxLength(20);
        
        builder.Property(u => u.Surname).HasMaxLength(50);

    }
}
