using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moongazing.Kernel.Messaging.Events;

public interface IEvent
{
    Guid Id { get; }
    DateTime CreatedAt { get; }
}
