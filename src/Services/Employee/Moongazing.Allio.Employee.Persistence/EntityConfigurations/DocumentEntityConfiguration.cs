using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Moongazing.Allio.Employee.Domain.Entities;
using Moongazing.Allio.Employee.Domain.Enums;

namespace Moongazing.Allio.Employee.Persistence.Configurations;

public class DocumentEntityConfiguration : IEntityTypeConfiguration<DocumentEntity>
{
    public void Configure(EntityTypeBuilder<DocumentEntity> builder)
    {
        builder.ToTable("Documents");

        builder.HasKey(d => d.Id);

        builder.Property(e => e.Id)
               .HasDefaultValueSql("NEWID()")
               .IsRequired();

        builder.Property(d => d.DocumentName)
               .IsRequired()
               .HasMaxLength(200);

        builder.Property(d => d.DocumentUrl)
               .IsRequired()
               .HasMaxLength(500);

        builder.Property(d => d.UploadedAt)
               .IsRequired()
               .HasColumnType("datetime2");

        builder.Property(d => d.Type)
               .IsRequired()
               .HasConversion<string>()
               .HasMaxLength(50);

        builder.Property(d => d.EmployeeId)
               .IsRequired();

        builder.HasOne(d => d.Employee)
               .WithMany(e => e.Documents)
               .HasForeignKey(d => d.EmployeeId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(er => er.ExpenseRecord)
           .WithOne()
           .HasForeignKey<DocumentEntity>(er => er.Id)
           .IsRequired(false)
           .OnDelete(DeleteBehavior.NoAction);
    }
}
