using Moongazing.Kernel.Messaging.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moongazing.Kernel.Messaging.Bus;

public interface IMessageBus
{
    Task PublishAsync<T>(T @event) where T : class;
    Task SendAsync<T>(string queueName, T message) where T : class;
}

