using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Moongazing.Allio.Employee.Domain.Entities;
using Moongazing.Allio.Employee.Domain.Enums;

namespace Moongazing.Allio.Employee.Persistence.Configurations;

public class LeaveRequestEntityConfiguration : IEntityTypeConfiguration<LeaveRequestEntity>
{
    public void Configure(EntityTypeBuilder<LeaveRequestEntity> builder)
    {
        builder.ToTable("LeaveRequests");


        builder.HasKey(lr => lr.Id);

        builder.Property(e => e.Id)
               .HasDefaultValueSql("NEWID()")
               .IsRequired();

        builder.Property(lr => lr.EmployeeId)
               .IsRequired();

        builder.Property(lr => lr.StartDate)
               .IsRequired()
               .HasColumnType("datetime2");

        builder.Property(lr => lr.EndDate)
               .IsRequired()
               .HasColumnType("datetime2");

        builder.Property(lr => lr.LeaveType)
               .IsRequired()
               .HasConversion<string>()
               .HasMaxLength(50);

        builder.Property(lr => lr.Reason)
               .IsRequired()
               .HasMaxLength(500);

        builder.Property(lr => lr.IsApproved)
               .IsRequired();

        builder.Property(lr => lr.RequestDate)
               .IsRequired()
               .HasColumnType("datetime2");

        builder.Property(lr => lr.ApprovedBy)
               .IsRequired()
               .HasMaxLength(150);

        builder.HasOne(lr => lr.Employee)
               .WithMany(e => e.LeaveRequests)
               .HasForeignKey(lr => lr.EmployeeId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
