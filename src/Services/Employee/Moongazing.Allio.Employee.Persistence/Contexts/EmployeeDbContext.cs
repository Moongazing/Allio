using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moongazing.Allio.Employee.Domain.Entities;
using Moongazing.Kernel.Persistence.Repositories;
using System.Reflection;

namespace Moongazing.Allio.Employee.Persistence.Contexts;

public class EmployeeDbContext : DbContext
{
    protected IConfiguration Configuration { get; set; }
    public virtual DbSet<EmployeeEntity> Employees { get; set; }
    public virtual DbSet<AdvanceRequestEntity> AdvanceRequests { get; set; }
    public virtual DbSet<AttendanceEntity> Attendances { get; set; }
    public virtual DbSet<AwardEntity> Awards { get; set; }
    public virtual DbSet<BankDetailEntity> BankDetails { get; set; }
    public virtual DbSet<BenefitEntity> Benefits { get; set; }
    public virtual DbSet<BranchEntity> Branches { get; set; }
    public virtual DbSet<DepartmentEntity> Departments { get; set; }
    public virtual DbSet<DisciplinaryActionEntity> DisciplinaryActions { get; set; }
    public virtual DbSet<DocumentEntity> Documents { get; set; }
    public virtual DbSet<EmergencyContactEntity> EmergencyContacts { get; set; }
    public virtual DbSet<ExpenseRecordEntity> ExpenseRecords { get; set; }
    public virtual DbSet<LeaveRequestEntity> LeaveRequests { get; set; }
    public virtual DbSet<OvertimeEntity> Overtimes { get; set; }
    public virtual DbSet<PerformanceReviewEntity> PerformanceReviews { get; set; }
    public virtual DbSet<PromotionEntity> Promotions { get; set; }
    public virtual DbSet<ShiftScheduleEntity> ShiftSchedules { get; set; }
    public virtual DbSet<TerminationEntity> Terminations { get; set; }
    public virtual DbSet<PayrollEntity> Payrolls { get; set; }


    public EmployeeDbContext(DbContextOptions<EmployeeDbContext> options, IConfiguration configuration) : base(options)
    {
        Configuration = configuration;
    }
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        ParseAudit();

        return await base.SaveChangesAsync(cancellationToken);

    }

    private void ParseAudit()
    {
        var entries = ChangeTracker.Entries<Entity<Guid>>()
                                   .Where(e => e.State == EntityState.Added ||
                                          e.State == EntityState.Modified ||
                                          e.State == EntityState.Deleted);

        foreach (var entry in entries)
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedDate = DateTime.UtcNow;
            }
            else if (entry.State == EntityState.Modified)
            {
                entry.Entity.UpdatedDate = DateTime.UtcNow;
            }
            else
            {
                entry.Entity.DeletedDate = DateTime.UtcNow;
            }
        }
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("allio");
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);

    }

}