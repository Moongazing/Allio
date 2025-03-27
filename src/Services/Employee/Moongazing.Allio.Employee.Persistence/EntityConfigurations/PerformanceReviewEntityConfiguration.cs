using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Moongazing.Allio.Employee.Domain.Entities;

namespace Moongazing.Allio.Employee.Persistence.EntityConfigurations;

public class PerformanceReviewEntityConfiguration : IEntityTypeConfiguration<PerformanceReviewEntity>
{
    public void Configure(EntityTypeBuilder<PerformanceReviewEntity> builder)
    {
        builder.ToTable("PerformanceReviews");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
               .HasDefaultValueSql("NEWID()")
               .IsRequired();

        builder.Property(p => p.EmployeeId)
               .IsRequired();

        builder.Property(p => p.ReviewDate)
               .IsRequired()
               .HasColumnType("datetime2");


        builder.Property(p => p.Reviewer)
               .IsRequired()
               .HasMaxLength(150);

        builder.Property(p => p.Score)
               .IsRequired();

        builder.Property(p => p.Comments)
               .IsRequired()
               .HasMaxLength(1000);

        builder.HasOne(p => p.Employee)
               .WithMany(e => e.PerformanceReviews)
               .HasForeignKey(p => p.EmployeeId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
