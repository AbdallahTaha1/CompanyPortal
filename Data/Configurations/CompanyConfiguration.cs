using CompanyPortal.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CompanyPortal.Data.Configurations
{
    public class CompanyConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.ArabicName)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(c => c.EnglishName)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(c => c.PhoneNumber)
                .HasMaxLength(20);

            builder.Property(c => c.WebsiteUrl)
                .HasMaxLength(300);

            builder.Property(c => c.LogoUrl)
                .HasMaxLength(300);

            // Relationships
            builder.HasOne(c => c.User)
                   .WithOne(u => u.Company)
                   .HasForeignKey<Company>(c => c.UserId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
