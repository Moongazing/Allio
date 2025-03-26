using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Moongazing.Allio.Employee.Domain.Entities;

namespace Moongazing.Allio.Employee.Persistence.Configurations;

public class BenefitEntityConfiguration : IEntityTypeConfiguration<BenefitEntity>
{
    public void Configure(EntityTypeBuilder<BenefitEntity> builder)
    {
        builder.ToTable("Benefits");

        builder.HasKey(b => b.Id);

        builder.Property(e => e.Id)
               .HasDefaultValueSql("NEWID()")
               .IsRequired();

        builder.Property(b => b.BenefitName)
               .IsRequired()
               .HasMaxLength(150);

        builder.Property(b => b.Description)
               .IsRequired()
               .HasMaxLength(500);

        builder.Property(b => b.Value)
               .IsRequired()
               .HasColumnType("decimal(18,2)");

        builder.Property(b => b.EmployeeId)
               .IsRequired();

        builder.HasOne(b => b.Employee)
               .WithMany(e => e.Benefits)
               .HasForeignKey(b => b.EmployeeId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
