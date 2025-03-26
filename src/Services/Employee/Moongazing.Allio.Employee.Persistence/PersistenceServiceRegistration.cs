using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moongazing.Allio.Employee.Application.Repositories;
using Moongazing.Allio.Employee.Persistence.Contexts;
using Moongazing.Allio.Employee.Persistence.Repositories;
using Polly;

namespace Moongazing.Allio.Employee.Persistence;

public static class PersistenceServiceRegistration
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {

        var retryPolicy = Policy
        .Handle<SqlException>()
        .WaitAndRetry([
                        TimeSpan.FromSeconds(10),
                        TimeSpan.FromSeconds(20),
                        TimeSpan.FromSeconds(30),
        ]);

        services.AddDbContext<EmployeeDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("Allio_Employee")));



        services.AddScoped<IAdvanceRequestRepository, AdvanceRequestRepository>();
        services.AddScoped<IAttendanceRepository, AttendanceRepository>();
        services.AddScoped<IAwardRepository, AwardRepository>();
        services.AddScoped<IBankDetailRepository, BankDetailRepository>();
        services.AddScoped<IBenefitRepository, BenefitRepository>();
        services.AddScoped<IBranchRepository, BranchRepository>();
        services.AddScoped<IDepartmentRepository, DepartmentRepository>();
        services.AddScoped<IDisciplinaryActionRepository, DisciplinaryActionRepository>();
        services.AddScoped<IDocumentRepository, DocumentRepository>();
        services.AddScoped<IEmergencyContactRepository, EmergencyContactRepository>();
        services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        services.AddScoped<IExpenseRecordRepository, ExpenseRecordRepository>();
        services.AddScoped<ILeaveRequestRepository, LeaveRequestRepository>();
        services.AddScoped<IOvertimeRepository, OvertimeRepository>();
        services.AddScoped<IPayrollRepository, PayrollRepository>();
        services.AddScoped<IPerformanceReviewRepository, PerformanceReviewRepository>();
        services.AddScoped<IProfessionRepository, ProfessionRepository>();
        services.AddScoped<IPromotionRepository, PromotionRepository>();
        services.AddScoped<IShiftScheduleRepository, ShiftScheduleRepository>();
        services.AddScoped<ITerminationRepository, TerminationRepository>();



        return services;
    }


}