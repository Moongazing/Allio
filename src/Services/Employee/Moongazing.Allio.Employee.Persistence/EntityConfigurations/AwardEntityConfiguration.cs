using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Moongazing.Allio.Employee.Domain.Entities;

namespace Moongazing.Allio.Employee.Persistence.EntityConfigurations;

public class AwardEntityConfiguration : IEntityTypeConfiguration<AwardEntity>
{
    public void Configure(EntityTypeBuilder<AwardEntity> builder)
    {
        builder.ToTable("Awards");


        builder.HasKey(a => a.Id);

        builder.Property(e => e.Id)
               .HasDefaultValueSql("NEWID()")
               .IsRequired();

        builder.Property(a => a.AwardName)
               .IsRequired()
               .HasMaxLength(150);

        builder.Property(a => a.Description)
               .IsRequired()
               .HasMaxLength(500);

        builder.Property(a => a.AwardDate)
               .IsRequired()
               .HasColumnType("datetime2");

        builder.Property(a => a.EmployeeId)
               .IsRequired();

        builder.HasOne(a => a.Employee)
               .WithMany(e => e.Awards)
               .HasForeignKey(a => a.EmployeeId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
