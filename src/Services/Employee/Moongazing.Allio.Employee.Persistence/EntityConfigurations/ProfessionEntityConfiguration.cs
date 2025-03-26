using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Moongazing.Allio.Employee.Domain.Entities;

namespace Moongazing.Allio.Employee.Persistence.Configurations;

public class ProfessionEntityConfiguration : IEntityTypeConfiguration<ProfessionEntity>
{
    public void Configure(EntityTypeBuilder<ProfessionEntity> builder)
    {
        builder.ToTable("Professions");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
               .HasDefaultValueSql("NEWID()")
               .IsRequired();

        builder.Property(p => p.Name)
               .IsRequired()
               .HasMaxLength(150);

        builder.Property(p => p.Description)
               .IsRequired()
               .HasMaxLength(500);

        builder.HasMany(p => p.Employees)
               .WithOne(e => e.Profession)
               .HasForeignKey(e => e.ProfessionId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
