using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Moongazing.Allio.Employee.Domain.Entities;

namespace Moongazing.Allio.Employee.Persistence.EntityConfigurations;

public class AdvanceRequestEntityConfiguration : IEntityTypeConfiguration<AdvanceRequestEntity>
{
    public void Configure(EntityTypeBuilder<AdvanceRequestEntity> builder)
    {
        builder.ToTable("AdvanceRequests");

        builder.HasKey(ar => ar.Id);

        builder.Property(e => e.Id)
               .HasDefaultValueSql("NEWID()")
               .IsRequired();

        builder.Property(ar => ar.EmployeeId)
               .IsRequired();


        builder.Property(ar => ar.RequestedAmount)
               .IsRequired()
               .HasColumnType("decimal(18,2)");

        builder.Property(ar => ar.ApprovedAmount)
               .IsRequired()
               .HasColumnType("decimal(18,2)");

        builder.Property(ar => ar.Reason)
               .IsRequired()
               .HasConversion<string>()
               .HasMaxLength(100);

        builder.Property(ar => ar.IsApproved)
               .IsRequired();

        builder.Property(ar => ar.RejecttReason)
               .HasMaxLength(500);

        builder.Property(ar => ar.RequestDate)
               .IsRequired()
               .HasColumnType("datetime2");

        builder.Property(ar => ar.ApprovalDate)
               .HasColumnType("datetime2");

        builder.HasOne(ar => ar.Employee)
               .WithMany(e => e.AdvanceRequests)
               .HasForeignKey(ar => ar.EmployeeId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
