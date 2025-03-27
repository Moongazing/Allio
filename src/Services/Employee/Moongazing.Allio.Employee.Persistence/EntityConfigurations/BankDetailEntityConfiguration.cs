using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Moongazing.Allio.Employee.Domain.Entities;

namespace Moongazing.Allio.Employee.Persistence.EntityConfigurations;

public class BankDetailEntityConfiguration : IEntityTypeConfiguration<BankDetailEntity>
{
    public void Configure(EntityTypeBuilder<BankDetailEntity> builder)
    {
        builder.ToTable("BankDetails");

        builder.HasKey(b => b.Id);

        builder.Property(e => e.Id)
               .HasDefaultValueSql("NEWID()")
               .IsRequired();

        builder.Property(b => b.BankName)
               .IsRequired()
               .HasMaxLength(150);

        builder.Property(b => b.AccountNumber)
               .IsRequired()
               .HasMaxLength(50);

        builder.Property(b => b.IBAN)
               .IsRequired()
               .HasMaxLength(34); 

        builder.Property(b => b.Currency)
               .IsRequired()
               .HasConversion<string>()
               .HasMaxLength(20);

        builder.Property(b => b.EmployeeId)
               .IsRequired();

        builder.HasOne(b => b.Employee)
               .WithMany(e => e.BankDetails)
               .HasForeignKey(b => b.EmployeeId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
