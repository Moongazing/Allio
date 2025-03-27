using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Moongazing.Allio.Employee.Domain.Entities;

namespace Moongazing.Allio.Employee.Persistence.EntityConfigurations;

public class ExpenseRecordEntityConfiguration : IEntityTypeConfiguration<ExpenseRecordEntity>
{
    public void Configure(EntityTypeBuilder<ExpenseRecordEntity> builder)
    {
        builder.ToTable("ExpenseRecords");

        builder.HasKey(er => er.Id);

        builder.Property(e => e.Id)
               .HasDefaultValueSql("NEWID()")
               .IsRequired();

        builder.Property(er => er.EmployeeId)
               .IsRequired();

        builder.Property(er => er.ExpenseType)
               .IsRequired()
               .HasConversion<string>()
               .HasMaxLength(50);

        builder.Property(er => er.Amount)
               .IsRequired()
               .HasColumnType("decimal(18,2)");

        builder.Property(er => er.Description)
               .IsRequired()
               .HasMaxLength(500);

        builder.Property(er => er.ExpenseDate)
               .IsRequired()
               .HasColumnType("datetime2");

        builder.Property(er => er.IsReimbursed)
               .IsRequired();

        builder.Property(er => er.DocumentId)
               .IsRequired()
               .HasMaxLength(500);

        builder.HasOne(er => er.Employee)
               .WithMany(e => e.ExpenseRecords)
               .HasForeignKey(er => er.EmployeeId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(er => er.Document)
               .WithOne()
               .HasForeignKey<ExpenseRecordEntity>(er => er.DocumentId)
               .IsRequired(false)
               .OnDelete(DeleteBehavior.NoAction);
    }
}
