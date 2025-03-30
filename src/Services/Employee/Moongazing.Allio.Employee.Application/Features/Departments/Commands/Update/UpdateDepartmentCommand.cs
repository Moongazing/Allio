using Mapster;
using MediatR;
using Moongazing.Allio.Employee.Application.Features.Departments.Rules;
using Moongazing.Allio.Employee.Application.Repositories;
using Moongazing.Allio.Employee.Domain.Entities;
using Moongazing.Kernel.Application.Pipelines.Caching;
using Moongazing.Kernel.Application.Pipelines.Logging;
using Moongazing.Kernel.Application.Pipelines.Performance;
using Moongazing.Kernel.Application.Pipelines.Transaction;

namespace Moongazing.Allio.Employee.Application.Features.Departments.Commands.Update;

public class UpdateDepartmentCommand:IRequest<UpdateDepartmentResponse>,
    ILoggableRequest, ICacheRemoverRequest, ITransactionalRequest, IIntervalRequest
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public bool BypassCache { get; }
    public string? CacheGroupKey => "Employee_Departments";
    public string? CacheKey => null;
    public int Interval => 15;


    public class UpdateDepartmentCommandHandler:IRequestHandler<UpdateDepartmentCommand,UpdateDepartmentResponse>
    {

        private readonly IDepartmentRepository departmentRepository;
        private readonly DepartmentBusinessRules departmentBusinessRules;
        public UpdateDepartmentCommandHandler(IDepartmentRepository departmentRepository, DepartmentBusinessRules departmentBusinessRules)
        {
            this.departmentRepository = departmentRepository;
            this.departmentBusinessRules = departmentBusinessRules;
        }
        public async Task<UpdateDepartmentResponse> Handle(UpdateDepartmentCommand request, CancellationToken cancellationToken)
        {
            DepartmentEntity? department = await departmentRepository.GetAsync(predicate: x => x.Id == request.Id,
                                                                         cancellationToken: cancellationToken);

            departmentBusinessRules.EnsureDepartmentExists(department);

            department = request.Adapt<DepartmentEntity>();

            var result = await departmentRepository.UpdateAsync(department, cancellationToken);

            UpdateDepartmentResponse response = result.Adapt<UpdateDepartmentResponse>();

            return response;
        }
    }
}
