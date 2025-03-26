using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Moongazing.Allio.Employee.Domain.Entities;

namespace Moongazing.Allio.Employee.Persistence.Configurations;

public class DisciplinaryActionEntityConfiguration : IEntityTypeConfiguration<DisciplinaryActionEntity>
{
    public void Configure(EntityTypeBuilder<DisciplinaryActionEntity> builder)
    {
        builder.ToTable("DisciplinaryActions");

        builder.HasKey(d => d.Id);

        builder.Property(e => e.Id)
               .HasDefaultValueSql("NEWID()")
               .IsRequired();

        builder.Property(d => d.EmployeeId)
               .IsRequired();

        builder.Property(d => d.ActionType)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(d => d.Reason)
               .IsRequired()
               .HasMaxLength(500);

        builder.Property(d => d.ActionDate)
               .IsRequired()
               .HasColumnType("datetime2");

        builder.Property(d => d.Comments)
               .IsRequired()
               .HasMaxLength(1000);

        builder.HasOne(d => d.Employee)
               .WithMany(e => e.DisciplinaryActions)
               .HasForeignKey(d => d.EmployeeId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
