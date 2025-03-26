using Moongazing.Allio.Employee.Domain.Enums;
using Moongazing.Kernel.Persistence.Repositories;

namespace Moongazing.Allio.Employee.Domain.Entities;

public class EmployeeEntity : Entity<Guid>
{

    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
    public DateTime DateOfBirth { get; set; }
    public string Address { get; set; } = default!;
    public string ProfilePictureUrl { get; set; } = default!;
    public EmployeeStatus Status { get; set; }
    public DateTime HireDate { get; set; }
    public Guid DepartmentId { get; set; }
    public Guid ProfessionId { get; set; }
    public Guid ManagerId { get; set; }
    public Guid BranchId { get; set; }
    public Guid BankId { get; set; }
    public decimal GrossSalary { get; set; }
    public decimal NetSalary { get; set; }
    public decimal TaxAmount { get; set; }
    public decimal Bonus { get; set; }


    
    public virtual BranchEntity Branch { get; set; } = default!;
    public virtual EmployeeEntity? Manager { get; set; }
    public virtual ProfessionEntity Profession { get; set; } = default!;
    public virtual DepartmentEntity Department { get; set; } = default!;
    public virtual ICollection<TerminationEntity> Terminations { get; set; } = new HashSet<TerminationEntity>();
    public virtual ICollection<BankDetailEntity> BankDetails { get; set; } = new HashSet<BankDetailEntity>();
    public virtual ICollection<LeaveRequestEntity> LeaveRequests { get; set; } = new HashSet<LeaveRequestEntity>();
    public virtual ICollection<AdvanceRequestEntity> AdvanceRequests { get; set; } = new HashSet<AdvanceRequestEntity>();
    public virtual ICollection<ExpenseRecordEntity> ExpenseRecords { get; set; } = new HashSet<ExpenseRecordEntity>();
    public virtual ICollection<OvertimeEntity> Overtimes { get; set; } = new HashSet<OvertimeEntity>();
    public virtual ICollection<PerformanceReviewEntity> PerformanceReviews { get; set; } = new HashSet<PerformanceReviewEntity>();
    public virtual ICollection<EmployeeEntity> Subordinates { get; set; } = new HashSet<EmployeeEntity>();
    public virtual ICollection<EmergencyContactEntity> EmergencyContacts { get; set; } = new HashSet<EmergencyContactEntity>();
    public virtual ICollection<DocumentEntity> Documents { get; set; } = new HashSet<DocumentEntity>();
    public virtual ICollection<ShiftScheduleEntity> ShiftSchedules { get; set; } = new HashSet<ShiftScheduleEntity>();
    public virtual ICollection<BenefitEntity> Benefits { get; set; } = new HashSet<BenefitEntity>();
    public virtual ICollection<PromotionEntity> Promotions { get; set; } = new HashSet<PromotionEntity>();
    public virtual ICollection<DisciplinaryActionEntity> DisciplinaryActions { get; set; } = new HashSet<DisciplinaryActionEntity>();
    public virtual ICollection<AwardEntity> Awards { get; set; } = new HashSet<AwardEntity>();
    public virtual ICollection<AttendanceEntity> Attendances { get; set; } = new HashSet<AttendanceEntity>();
    public virtual ICollection<PayrollEntity> Payrolls { get; set; } = new HashSet<PayrollEntity>();

    public EmployeeEntity()
    {

    }
}