using Mapster;
using MediatR;
using Moongazing.Allio.Employee.Application.Features.Departments.Rules;
using Moongazing.Allio.Employee.Application.Repositories;
using Moongazing.Allio.Employee.Domain.Entities;
using Moongazing.Kernel.Application.Pipelines.Caching;
using Moongazing.Kernel.Application.Pipelines.Logging;
using Moongazing.Kernel.Application.Pipelines.Performance;

namespace Moongazing.Allio.Employee.Application.Features.Departments.Queries.GetById;

public class GetDepartmentByIdQuery : IRequest<GetDepartmentByIdResponse>,
    ILoggableRequest, IIntervalRequest, ICachableRequest
{
    public Guid Id { get; set; } = default!;
    public string CacheKey => $"{GetType().Name}({Id})";
    public bool BypassCache { get; }
    public string? CacheGroupKey => "Employee_Departments";
    public TimeSpan? SlidingExpiration { get; }
    public int Interval => 15;


    public class GetDepartmentByIdQueryHandler : IRequestHandler<GetDepartmentByIdQuery, GetDepartmentByIdResponse>
    {
        private readonly IDepartmentRepository departmentRepository;
        private readonly DepartmentBusinessRules departmentBusinessRules;
        public GetDepartmentByIdQueryHandler(IDepartmentRepository departmentRepository,
                                            DepartmentBusinessRules departmentBusinessRules)
        {
            this.departmentRepository = departmentRepository;
            this.departmentBusinessRules = departmentBusinessRules;
        }
        public async Task<GetDepartmentByIdResponse> Handle(GetDepartmentByIdQuery request, CancellationToken cancellationToken)
        {
            DepartmentEntity? department = await departmentRepository.GetAsync(
                predicate: x => x.Id == request.Id,
                withDeleted: false,
                cancellationToken: cancellationToken);

            departmentBusinessRules.EnsureDepartmentExists(department);

            GetDepartmentByIdResponse response = department!.Adapt<GetDepartmentByIdResponse>();

            return response;
        }
    }
}
