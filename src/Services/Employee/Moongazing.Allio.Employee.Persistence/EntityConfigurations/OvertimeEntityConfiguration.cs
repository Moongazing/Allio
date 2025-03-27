using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Moongazing.Allio.Employee.Domain.Entities;

namespace Moongazing.Allio.Employee.Persistence.EntityConfigurations;

public class OvertimeEntityConfiguration : IEntityTypeConfiguration<OvertimeEntity>
{
    public void Configure(EntityTypeBuilder<OvertimeEntity> builder)
    {
        builder.ToTable("Overtimes");

        builder.HasKey(o => o.Id);

        builder.Property(e => e.Id)
               .HasDefaultValueSql("NEWID()")
               .IsRequired();

        builder.Property(o => o.EmployeeId)
               .IsRequired();

        builder.Property(o => o.StartTime)
               .IsRequired()
               .HasColumnType("datetime2");

        builder.Property(o => o.EndTime)
               .IsRequired()
               .HasColumnType("datetime2");

        builder.Property(o => o.HourlyRate)
               .IsRequired()
               .HasColumnType("decimal(18,2)");

        builder.Property(o => o.TotalAmount)
               .IsRequired()
               .HasColumnType("decimal(18,2)");

        builder.HasOne(o => o.Employee)
               .WithMany(e => e.Overtimes)
               .HasForeignKey(o => o.EmployeeId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
