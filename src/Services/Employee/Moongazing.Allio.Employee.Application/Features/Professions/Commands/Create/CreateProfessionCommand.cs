using FluentValidation;
using MediatR;
using Moongazing.Kernel.Application.Pipelines.Caching;
using Moongazing.Kernel.Application.Pipelines.Logging;
using Moongazing.Kernel.Application.Pipelines.Performance;
using Moongazing.Kernel.Application.Pipelines.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moongazing.Allio.Employee.Application.Features.Professions.Commands.Create;

public class CreateProfessionCommand : IRequest<CreateProfessionResponse>, ILoggableRequest, ICacheRemoverRequest, ITransactionalRequest, IIntervalRequest
{
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public bool BypassCache { get; }
    public string? CacheGroupKey => "Employee_Profession";
    public string? CacheKey => null;
    public int Interval => 15;
}

public class CreateProfessionCommandValidator : AbstractValidator<CreateProfessionCommand>
{
    public CreateProfessionCommandValidator()
    {

    }
}