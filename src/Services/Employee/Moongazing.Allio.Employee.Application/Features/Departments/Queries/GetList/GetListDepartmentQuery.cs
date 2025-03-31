using Mapster;
using MediatR;
using Moongazing.Allio.Employee.Application.Repositories;
using Moongazing.Allio.Employee.Domain.Entities;
using Moongazing.Kernel.Application.Pipelines.Caching;
using Moongazing.Kernel.Application.Pipelines.Logging;
using Moongazing.Kernel.Application.Pipelines.Performance;
using Moongazing.Kernel.Application.Requests;
using Moongazing.Kernel.Application.Responses;
using Moongazing.Kernel.Persistence.Paging;

namespace Moongazing.Allio.Employee.Application.Features.Departments.Queries.GetList;

public class GetListDepartmentQuery : IRequest<PaginatedResponse<GetListDepartmentResponse>>,
    ILoggableRequest, IIntervalRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; } = default!;
    public string CacheKey => $"{GetType().Name}({PageRequest.PageIndex}-{PageRequest.PageSize})";
    public bool BypassCache { get; }
    public string? CacheGroupKey => "Employee_Departments";
    public TimeSpan? SlidingExpiration { get; }
    public int Interval => 15;

    public class GetListDepartmentQueryHandler : IRequestHandler<GetListDepartmentQuery, PaginatedResponse<GetListDepartmentResponse>>
    {
        private readonly IDepartmentRepository departmentRepository;

        public GetListDepartmentQueryHandler(IDepartmentRepository departmentRepository)
        {
            this.departmentRepository = departmentRepository;
        }

        public async Task<PaginatedResponse<GetListDepartmentResponse>> Handle(GetListDepartmentQuery request, CancellationToken cancellationToken)
        {
            IPaginate<DepartmentEntity>? departments = await departmentRepository.GetListAsync(index: request.PageRequest.PageIndex,
                                                                                                size: request.PageRequest.PageSize,
                                                                                                withDeleted: false,
                                                                                                cancellationToken: cancellationToken);

            PaginatedResponse<GetListDepartmentResponse> response = departments.Adapt<PaginatedResponse<GetListDepartmentResponse>>();

            return response;
        }
    }
}
