using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Moongazing.Allio.Employee.Domain.Entities;

namespace Moongazing.Allio.Employee.Persistence.EntityConfigurations;

public class AttendanceEntityConfiguration : IEntityTypeConfiguration<AttendanceEntity>
{
    public void Configure(EntityTypeBuilder<AttendanceEntity> builder)
    {
        builder.ToTable("Attendances");

        builder.HasKey(a => a.Id);

        builder.Property(e => e.Id)
               .HasDefaultValueSql("NEWID()")
               .IsRequired();

        builder.Property(a => a.EmployeeId)
               .IsRequired();

        builder.Property(a => a.CheckInTime)
               .IsRequired()
               .HasColumnType("datetime2");

        builder.Property(a => a.CheckOutTime)
               .HasColumnType("datetime2");

        builder.Property(a => a.Status)
               .IsRequired()
               .HasConversion<string>()
               .HasMaxLength(50);

        builder.HasOne(a => a.Employee)
               .WithMany(e => e.Attendances)
               .HasForeignKey(a => a.EmployeeId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
