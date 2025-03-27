using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Moongazing.Allio.Employee.Domain.Entities;

namespace Moongazing.Allio.Employee.Persistence.EntityConfigurations;

public class BranchEntityConfiguration : IEntityTypeConfiguration<BranchEntity>
{
    public void Configure(EntityTypeBuilder<BranchEntity> builder)
    {
        builder.ToTable("Branches");

        builder.HasKey(b => b.Id);

        builder.Property(e => e.Id)
               .HasDefaultValueSql("NEWID()")
               .IsRequired();

        builder.Property(b => b.Name)
               .IsRequired()
               .HasMaxLength(150);

        builder.Property(b => b.Address)
               .IsRequired()
               .HasMaxLength(300);

        builder.Property(b => b.City)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(b => b.Country)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(b => b.CountryPhoneCode)
               .IsRequired()
               .HasConversion<string>()
               .HasMaxLength(10);

        builder.Property(b => b.PhoneNumber)
               .IsRequired()
               .HasMaxLength(20);

        builder.Property(b => b.DepartmentId)
               .IsRequired();

        builder.HasOne(b => b.Department)
               .WithMany(d => d.Branches)
               .HasForeignKey(b => b.DepartmentId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(b => b.Employees)
               .WithOne(e => e.Branch)
               .HasForeignKey(e => e.BranchId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
