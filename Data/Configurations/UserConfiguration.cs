using CompanyPortal.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CompanyPortal.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);

            builder.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(200);

            builder.HasIndex(u => u.Email)
                .IsUnique();

            builder.Property(u => u.PasswordHash)
                .HasMaxLength(500);

            builder.Property(u => u.OtpCode)
                .HasMaxLength(20);

            builder.Property(u => u.IsVerified)
                .HasDefaultValue(false);

            builder.Property(u => u.Role)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}
