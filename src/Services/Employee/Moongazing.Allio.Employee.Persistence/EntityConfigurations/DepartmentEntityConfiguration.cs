using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Moongazing.Allio.Employee.Domain.Entities;

namespace Moongazing.Allio.Employee.Persistence.Configurations;

public class DepartmentEntityConfiguration : IEntityTypeConfiguration<DepartmentEntity>
{
    public void Configure(EntityTypeBuilder<DepartmentEntity> builder)
    {
        builder.ToTable("Departments");

        builder.HasKey(d => d.Id);

        builder.Property(e => e.Id)
               .HasDefaultValueSql("NEWID()")
               .IsRequired();

        builder.Property(d => d.Name)
               .IsRequired()
               .HasMaxLength(150);

        builder.Property(d => d.Description)
               .IsRequired()
               .HasMaxLength(500);


        builder.HasMany(d => d.Employees)
               .WithOne(e => e.Department)
               .HasForeignKey(e => e.DepartmentId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(d => d.Branches)
               .WithOne(b => b.Department)
               .HasForeignKey(b => b.DepartmentId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
