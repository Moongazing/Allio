using Mapster;
using MediatR;
using Moongazing.Allio.Employee.Application.Repositories;
using Moongazing.Allio.Employee.Domain.Entities;
using Moongazing.Kernel.Application.Pipelines.Logging;
using Moongazing.Kernel.Application.Pipelines.Performance;
using Moongazing.Kernel.Application.Requests;
using Moongazing.Kernel.Application.Responses;
using Moongazing.Kernel.Persistence.Dynamic;
using Moongazing.Kernel.Persistence.Paging;

namespace Moongazing.Allio.Employee.Application.Features.Departments.Queries.GetListByDynamic;

public class GetListDepartmentByDynamicQuery : IRequest<PaginatedResponse<GetListDepartmentByDynamicResponse>>,
      ILoggableRequest, IIntervalRequest
{
    public DynamicQuery DynamicQuery { get; set; } = default!;
    public PageRequest PageRequest { get; set; } = default!;
    public int Interval => 15;

    public class GetListDepartmentByDynamicQueryHandler : IRequestHandler<GetListDepartmentByDynamicQuery, PaginatedResponse<GetListDepartmentByDynamicResponse>>
    {
        private readonly IDepartmentRepository departmentRepository;

        public GetListDepartmentByDynamicQueryHandler(IDepartmentRepository departmentRepository)
        {
            this.departmentRepository = departmentRepository;
        }

        public async Task<PaginatedResponse<GetListDepartmentByDynamicResponse>> Handle(GetListDepartmentByDynamicQuery request, CancellationToken cancellationToken)
        {
            IPaginate<DepartmentEntity>? departments = await departmentRepository.GetListByDynamicAsync(
                dynamic: request.DynamicQuery,
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                withDeleted: false,
                cancellationToken: cancellationToken);

            PaginatedResponse<GetListDepartmentByDynamicResponse> response = departments.Adapt<PaginatedResponse<GetListDepartmentByDynamicResponse>>();

            return response;
        }
    }
}
