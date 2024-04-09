using DayBook.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DayBook.DAL.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(u => u.Id).ValueGeneratedOnAdd();
        builder.Property(u => u.Login).IsRequired().HasMaxLength(100);
        builder.Property(u => u.Password).IsRequired();

        builder.HasMany<Report>(u => u.Reports)
            .WithOne(u => u.User)
            .HasForeignKey(u => u.UserId)
            .HasPrincipalKey(u => u.Id);

        builder.HasData(new List<User>()
        {
            new User()
            {
                Id = 1,
                Login = "Admin",
                Password = "Admin123!",
                CreatedAt = DateTime.UtcNow
            }
        });
    }
}
