using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Moongazing.Allio.Employee.Domain.Entities;

namespace Moongazing.Allio.Employee.Persistence.Configurations;

public class PayrollEntityConfiguration : IEntityTypeConfiguration<PayrollEntity>
{
    public void Configure(EntityTypeBuilder<PayrollEntity> builder)
    {
        builder.ToTable("Payrolls");

        builder.HasKey(p => p.Id);

        builder.Property(e => e.Id)
               .HasDefaultValueSql("NEWID()")
               .IsRequired();

        builder.Property(p => p.EmployeeId)
               .IsRequired();

        builder.Property(p => p.PayDate)
               .IsRequired()
               .HasColumnType("datetime2");

        builder.Property(p => p.GrossSalary)
               .IsRequired()
               .HasColumnType("decimal(18,2)");

        builder.Property(p => p.NetSalary)
               .IsRequired()
               .HasColumnType("decimal(18,2)");

        builder.Property(p => p.TaxDeduction)
               .IsRequired()
               .HasColumnType("decimal(18,2)");

        builder.Property(p => p.Bonus)
               .IsRequired()
               .HasColumnType("decimal(18,2)");

        builder.HasOne(p => p.Employee)
               .WithMany(e => e.Payrolls)
               .HasForeignKey(p => p.EmployeeId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
