using Mapster;
using MediatR;
using Moongazing.Allio.Employee.Application.Features.Departments.Rules;
using Moongazing.Allio.Employee.Application.Features.Employees.Rules;
using Moongazing.Allio.Employee.Application.Repositories;
using Moongazing.Allio.Employee.Domain.Entities;
using Moongazing.Kernel.Application.Pipelines.Caching;
using Moongazing.Kernel.Application.Pipelines.Logging;
using Moongazing.Kernel.Application.Pipelines.Performance;

namespace Moongazing.Allio.Employee.Application.Features.Departments.Queries.GetByDepartmentManager;

public class GetDepartmentByManagerIdQuery : IRequest<GetDepartmentByManagerIdResponse>,
    ILoggableRequest, IIntervalRequest, ICachableRequest

{
    public Guid DepartmentManagerId { get; set; } = default!;
    public string CacheKey => $"{GetType().Name}({DepartmentManagerId})";
    public bool BypassCache { get; }
    public string? CacheGroupKey => "Employee_Departments";
    public TimeSpan? SlidingExpiration { get; }
    public int Interval => 15;

    public class GetDepartmentByManagerIdQueryHandler : IRequestHandler<GetDepartmentByManagerIdQuery, GetDepartmentByManagerIdResponse>
    {
        private readonly IDepartmentRepository departmentRepository;
        private readonly DepartmentBusinessRules departmentBusinessRules;
        private readonly EmployeeBusinessRules employeeBusinessRules;

        public GetDepartmentByManagerIdQueryHandler(IDepartmentRepository departmentRepository,
                                                    DepartmentBusinessRules departmentBusinessRules,
                                                    EmployeeBusinessRules employeeBusinessRules)
        {
            this.departmentRepository = departmentRepository;
            this.departmentBusinessRules = departmentBusinessRules;
            this.employeeBusinessRules = employeeBusinessRules;
        }

        public async Task<GetDepartmentByManagerIdResponse> Handle(GetDepartmentByManagerIdQuery request, CancellationToken cancellationToken)
        {

            await employeeBusinessRules.EnsureEmployeeExistsAsync(request.DepartmentManagerId);

            DepartmentEntity? department = await departmentRepository.GetAsync(predicate: x => x.DepartmentManagerId == request.DepartmentManagerId,
                                                                               withDeleted: false,
                                                                               cancellationToken: cancellationToken);
            departmentBusinessRules.EnsureDepartmentExists(department);

            GetDepartmentByManagerIdResponse response = department!.Adapt<GetDepartmentByManagerIdResponse>();
            return response;
        }
    }
}
