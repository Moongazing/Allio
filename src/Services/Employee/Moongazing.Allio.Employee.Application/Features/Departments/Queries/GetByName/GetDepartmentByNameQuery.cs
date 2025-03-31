using Mapster;
using MediatR;
using Moongazing.Allio.Employee.Application.Features.Departments.Rules;
using Moongazing.Allio.Employee.Application.Repositories;
using Moongazing.Allio.Employee.Domain.Entities;
using Moongazing.Kernel.Application.Pipelines.Caching;
using Moongazing.Kernel.Application.Pipelines.Logging;
using Moongazing.Kernel.Application.Pipelines.Performance;

namespace Moongazing.Allio.Employee.Application.Features.Departments.Queries.GetByName;

public class GetDepartmentByNameQuery : IRequest<GetDepartmentByNameResponse>, ILoggableRequest, IIntervalRequest, ICachableRequest
{
    public string Name { get; set; } = default!;
    public string CacheKey => $"{GetType().Name}({Name})";
    public bool BypassCache { get; }
    public string? CacheGroupKey => "Employee_Departments";
    public TimeSpan? SlidingExpiration { get; }
    public int Interval => 15;


    public class GetDepartmentByNameQueryHandler : IRequestHandler<GetDepartmentByNameQuery, GetDepartmentByNameResponse>
    {
        private readonly IDepartmentRepository departmentRepository;
        private readonly DepartmentBusinessRules departmentBusinessRules;

        public GetDepartmentByNameQueryHandler(IDepartmentRepository departmentRepository, DepartmentBusinessRules departmentBusinessRules)
        {
            this.departmentRepository = departmentRepository;
            this.departmentBusinessRules = departmentBusinessRules;
        }

        public async Task<GetDepartmentByNameResponse> Handle(GetDepartmentByNameQuery request, CancellationToken cancellationToken)
        {

            DepartmentEntity? department = await departmentRepository.GetAsync(predicate: x => x.Name.ToLower() == request.Name.ToLower(),
                                                                               withDeleted: false,
                                                                               cancellationToken: cancellationToken);
            departmentBusinessRules.EnsureDepartmentExists(department);

            await departmentBusinessRules.EnsureDepartmentNameIsUnique(request.Name);

            GetDepartmentByNameResponse response = department!.Adapt<GetDepartmentByNameResponse>();

            return response;
        }
    }
}
