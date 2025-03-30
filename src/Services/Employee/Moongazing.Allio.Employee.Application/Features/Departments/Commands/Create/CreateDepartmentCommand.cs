using Mapster;
using MediatR;
using Moongazing.Allio.Employee.Application.Features.Departments.Rules;
using Moongazing.Allio.Employee.Application.Features.Employees.Rules;
using Moongazing.Allio.Employee.Application.Repositories;
using Moongazing.Allio.Employee.Domain.Entities;
using Moongazing.Kernel.Application.Pipelines.Caching;
using Moongazing.Kernel.Application.Pipelines.Logging;
using Moongazing.Kernel.Application.Pipelines.Performance;
using Moongazing.Kernel.Application.Pipelines.Transaction;

namespace Moongazing.Allio.Employee.Application.Features.Departments.Commands.Create;

public class CreateDepartmentCommand : IRequest<CreateDepartmentResponse>,
    ILoggableRequest, ICacheRemoverRequest, ITransactionalRequest, IIntervalRequest
{
    public Guid DepartmentManagerId { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public bool BypassCache { get; }
    public string? CacheGroupKey => "Employee_Departments";
    public string? CacheKey => null;
    public int Interval => 15;

    public class CreateDepartmentCommandHandler : IRequestHandler<CreateDepartmentCommand, CreateDepartmentResponse>
    {
        private readonly IDepartmentRepository departmentRepository;
        private readonly DepartmentBusinessRules departmentBusinessRules;
        private readonly EmployeeBusinessRules employeeBusinessRules;

        public CreateDepartmentCommandHandler(IDepartmentRepository departmentRepository,
                                              DepartmentBusinessRules departmentBusinessRules,
                                              EmployeeBusinessRules employeeBusinessRules)
        {
            this.departmentRepository = departmentRepository;
            this.departmentBusinessRules = departmentBusinessRules;
            this.employeeBusinessRules = employeeBusinessRules;
        }

        public async Task<CreateDepartmentResponse> Handle(CreateDepartmentCommand request, CancellationToken cancellationToken)
        {
            await employeeBusinessRules.EnsureEmployeeExistsAsync(request.DepartmentManagerId);
            await departmentBusinessRules.EnsureDepartmentNameIsUnique(request.Name);

            DepartmentEntity? departmentToAdd = request.Adapt<DepartmentEntity>();

            var result = await departmentRepository.AddAsync(departmentToAdd, cancellationToken);

            CreateDepartmentResponse response = result.Adapt<CreateDepartmentResponse>();

            return response;
        }
    }
}
