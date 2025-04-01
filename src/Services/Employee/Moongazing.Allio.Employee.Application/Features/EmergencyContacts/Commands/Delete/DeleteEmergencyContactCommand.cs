using Mapster;
using MediatR;
using Moongazing.Allio.Employee.Application.Features.EmergencyContacts.Rules;
using Moongazing.Allio.Employee.Application.Repositories;
using Moongazing.Allio.Employee.Domain.Entities;
using Moongazing.Kernel.Application.Pipelines.Caching;
using Moongazing.Kernel.Application.Pipelines.Logging;
using Moongazing.Kernel.Application.Pipelines.Performance;
using Moongazing.Kernel.Application.Pipelines.Transaction;

namespace Moongazing.Allio.Employee.Application.Features.EmergencyContacts.Commands.Delete;

public class DeleteEmergencyContactCommand:IRequest<DeleteEmergencyContactResponse>, 
	ILoggableRequest, ICacheRemoverRequest, ITransactionalRequest, IIntervalRequest

{
    public Guid Id { get; set; }
    public bool BypassCache { get; }
    public string? CacheGroupKey => "Employee_EmergencyContacts";
    public string? CacheKey => null;
    public int Interval => 15;


    public class DeleteEmergencyContactCommandHandler : IRequestHandler<DeleteEmergencyContactCommand, DeleteEmergencyContactResponse>
    {   
        private readonly IEmergencyContactRepository emergencyContactRepository;
        private readonly EmergencyContactBusinessRules emergencyContactBusinessRules;

        public DeleteEmergencyContactCommandHandler(IEmergencyContactRepository emergencyContactRepository, EmergencyContactBusinessRules emergencyContactBusinessRules)
        {
            this.emergencyContactRepository = emergencyContactRepository;
            this.emergencyContactBusinessRules = emergencyContactBusinessRules;
        }

        public async Task<DeleteEmergencyContactResponse> Handle(DeleteEmergencyContactCommand request, CancellationToken cancellationToken)
        {
            EmergencyContactEntity? emergencyContact = await emergencyContactRepository.GetAsync(predicate: x => x.Id == request.Id,
                                                                                                 cancellationToken: cancellationToken);

            emergencyContactBusinessRules.EnsureEmergencyContanctExists(emergencyContact);

            await emergencyContactRepository.DeleteAsync(emergencyContact!, cancellationToken: cancellationToken, permanent: false);

            DeleteEmergencyContactResponse response = emergencyContact.Adapt<DeleteEmergencyContactResponse>();

            return response;
        }
    }
}
