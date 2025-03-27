using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Moongazing.Allio.Employee.Domain.Entities;

namespace Moongazing.Allio.Employee.Persistence.EntityConfigurations;

public class PromotionEntityConfiguration : IEntityTypeConfiguration<PromotionEntity>
{
    public void Configure(EntityTypeBuilder<PromotionEntity> builder)
    {
        builder.ToTable("Promotions");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
               .HasDefaultValueSql("NEWID()")
               .IsRequired();

        builder.Property(p => p.EmployeeId)
               .IsRequired();

        builder.Property(p => p.NewPosition)
               .IsRequired()
               .HasMaxLength(150);


        builder.Property(p => p.PromotionDate)
               .IsRequired()
               .HasColumnType("datetime2");


        builder.Property(p => p.ApprovedBy)
               .IsRequired()
               .HasMaxLength(150);


        builder.Property(p => p.Comments)
               .IsRequired()
               .HasMaxLength(1000);

        builder.HasOne(p => p.Employee)
               .WithMany(e => e.Promotions)
               .HasForeignKey(p => p.EmployeeId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
