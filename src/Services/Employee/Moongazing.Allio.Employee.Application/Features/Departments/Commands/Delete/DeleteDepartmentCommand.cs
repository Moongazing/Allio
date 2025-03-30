using Mapster;
using MediatR;
using Moongazing.Allio.Employee.Application.Features.Departments.Rules;
using Moongazing.Allio.Employee.Application.Repositories;
using Moongazing.Allio.Employee.Domain.Entities;
using Moongazing.Kernel.Application.Pipelines.Caching;
using Moongazing.Kernel.Application.Pipelines.Logging;
using Moongazing.Kernel.Application.Pipelines.Performance;
using Moongazing.Kernel.Application.Pipelines.Transaction;

namespace Moongazing.Allio.Employee.Application.Features.Departments.Commands.Delete;

public class DeleteDepartmentCommand:IRequest<DeleteDepartmentResponse>,
    ILoggableRequest, ICacheRemoverRequest, ITransactionalRequest, IIntervalRequest
{
    public Guid Id { get; set; }
    public bool BypassCache { get; }
    public string? CacheGroupKey => "Employee_Departments";
    public string? CacheKey => null;
    public int Interval => 15;


    public class DeleteDepartmentCommandHandler : IRequestHandler<DeleteDepartmentCommand, DeleteDepartmentResponse>
    {
        private readonly IDepartmentRepository departmentRepository;
        private readonly DepartmentBusinessRules departmentBusinessRules;

        public DeleteDepartmentCommandHandler(IDepartmentRepository departmentRepository, DepartmentBusinessRules departmentBusinessRules)
        {
            this.departmentRepository = departmentRepository;
            this.departmentBusinessRules = departmentBusinessRules;
        }

        public async Task<DeleteDepartmentResponse> Handle(DeleteDepartmentCommand request, CancellationToken cancellationToken)
        {
            DepartmentEntity? departmentToDelete = await departmentRepository.GetAsync(predicate: x => x.Id == request.Id,
                                                                                      cancellationToken: cancellationToken);

            departmentBusinessRules.EnsureDepartmentExists(departmentToDelete);

            await departmentRepository.DeleteAsync(departmentToDelete!, cancellationToken: cancellationToken, permanent: false);

            DeleteDepartmentResponse response = departmentToDelete.Adapt<DeleteDepartmentResponse>();

            return response;
        }
    }
}
