using DayBook.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DayBook.DAL.Configurations
{
    public class ReportConfiguration : IEntityTypeConfiguration<Report>
    {
        public void Configure(EntityTypeBuilder<Report> builder)
        {
            builder.Property(r => r.Id).ValueGeneratedOnAdd();
            builder.Property(r => r.Name).IsRequired().HasMaxLength(100);
            builder.Property(r => r.Description).IsRequired().HasMaxLength(2000);

            builder.HasData(new List<Report>()
            {
                new Report()
                {
                    Id = 1,
                    Name = "Admin",
                    Description = "Admin123!",
                    UserId = 1,
                    CreatedAt = DateTime.UtcNow
                }
            });
        }
    }
}
