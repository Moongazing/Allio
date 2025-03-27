using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Moongazing.Allio.Employee.Domain.Entities;

namespace Moongazing.Allio.Employee.Persistence.EntityConfigurations;

public class EmployeeEntityConfiguration : IEntityTypeConfiguration<EmployeeEntity>
{
    public void Configure(EntityTypeBuilder<EmployeeEntity> builder)
    {
        builder.ToTable("Employees");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
               .HasDefaultValueSql("NEWID()")
               .IsRequired();

        builder.Property(e => e.FirstName)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(e => e.LastName)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(e => e.Email)
               .IsRequired()
               .HasMaxLength(150);

        builder.Property(e => e.PhoneNumber)
               .IsRequired()
               .HasMaxLength(20);

        builder.Property(e => e.DateOfBirth)
               .IsRequired()
               .HasColumnType("datetime2");

        builder.Property(e => e.Address)
               .IsRequired()
               .HasMaxLength(300);

        builder.Property(e => e.ProfilePictureUrl)
               .IsRequired()
               .HasMaxLength(500);

        builder.Property(e => e.Status)
               .IsRequired()
               .HasConversion<string>()
               .HasMaxLength(50);

        builder.Property(e => e.HireDate)
               .IsRequired()
               .HasColumnType("datetime2");

        builder.Property(e => e.GrossSalary)
               .IsRequired()
               .HasColumnType("decimal(18,2)");

        builder.Property(e => e.NetSalary)
               .IsRequired()
               .HasColumnType("decimal(18,2)");

        builder.Property(e => e.TaxAmount)
               .IsRequired()
               .HasColumnType("decimal(18,2)");

        builder.Property(e => e.Bonus)
               .IsRequired()
               .HasColumnType("decimal(18,2)");

        builder.Property(e => e.DepartmentId).IsRequired();
        builder.Property(e => e.ProfessionId).IsRequired();
        builder.Property(e => e.BranchId).IsRequired();
        builder.Property(e => e.ManagerId).IsRequired();
        builder.Property(e => e.BankId).IsRequired();

        builder.HasOne(e => e.Department)
               .WithMany(d => d.Employees)
               .HasForeignKey(e => e.DepartmentId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.Profession)
               .WithMany(p => p.Employees)
               .HasForeignKey(e => e.ProfessionId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.Branch)
               .WithMany(b => b.Employees)
               .HasForeignKey(e => e.BranchId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.Manager)
               .WithMany(m => m.Subordinates)
               .HasForeignKey(e => e.ManagerId)
               .OnDelete(DeleteBehavior.Restrict);

      
    }
}
