using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Moongazing.Allio.Employee.Domain.Entities;

namespace Moongazing.Allio.Employee.Persistence.Configurations;

public class EmergencyContactEntityConfiguration : IEntityTypeConfiguration<EmergencyContactEntity>
{
    public void Configure(EntityTypeBuilder<EmergencyContactEntity> builder)
    {
        builder.ToTable("EmergencyContacts");

        builder.HasKey(ec => ec.Id);

        builder.Property(e => e.Id)
               .HasDefaultValueSql("NEWID()")
               .IsRequired();

        builder.Property(ec => ec.Name)
               .IsRequired()
               .HasMaxLength(150);

        builder.Property(ec => ec.PhoneNumber)
               .IsRequired()
               .HasMaxLength(20);

        builder.Property(ec => ec.Relation)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(ec => ec.EmployeeId)
               .IsRequired();

        builder.HasOne(ec => ec.Employee)
               .WithMany(e => e.EmergencyContacts)
               .HasForeignKey(ec => ec.EmployeeId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
