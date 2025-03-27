using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Moongazing.Allio.Employee.Domain.Entities;

namespace Moongazing.Allio.Employee.Persistence.EntityConfigurations;

public class TerminationEntityConfiguration : IEntityTypeConfiguration<TerminationEntity>
{
    public void Configure(EntityTypeBuilder<TerminationEntity> builder)
    {
        builder.ToTable("Terminations");

        builder.HasKey(t => t.Id);

        builder.Property(t => t.Id)
               .HasDefaultValueSql("NEWID()")
               .IsRequired();

        builder.Property(t => t.EmployeeId)
               .IsRequired();

        builder.Property(t => t.Reason)
               .IsRequired()
               .HasMaxLength(500);

        builder.Property(t => t.TerminationDate)
               .IsRequired()
               .HasColumnType("datetime2");

        builder.Property(t => t.Comments)
               .IsRequired()
               .HasMaxLength(1000);

        builder.Property(t => t.IsVoluntary)
               .IsRequired();

        builder.HasOne(t => t.Employee)
               .WithMany(e => e.Terminations)
               .HasForeignKey(t => t.EmployeeId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
