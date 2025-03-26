using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Moongazing.Allio.Employee.Domain.Entities;

namespace Moongazing.Allio.Employee.Persistence.Configurations;

public class ShiftScheduleEntityConfiguration : IEntityTypeConfiguration<ShiftScheduleEntity>
{
    public void Configure(EntityTypeBuilder<ShiftScheduleEntity> builder)
    {
        builder.ToTable("ShiftSchedules");

        builder.HasKey(s => s.Id);

        builder.Property(s => s.Id)
               .HasDefaultValueSql("NEWID()")
               .IsRequired();


        builder.Property(s => s.EmployeeId)
               .IsRequired();


        builder.Property(s => s.ShiftStart)
               .IsRequired()
               .HasColumnType("datetime2");

        builder.Property(s => s.ShiftEnd)
               .IsRequired()
               .HasColumnType("datetime2");

        builder.Property(s => s.Notes)
               .IsRequired()
               .HasMaxLength(1000);


        builder.HasOne(s => s.Employee)
               .WithMany(e => e.ShiftSchedules)
               .HasForeignKey(s => s.EmployeeId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
