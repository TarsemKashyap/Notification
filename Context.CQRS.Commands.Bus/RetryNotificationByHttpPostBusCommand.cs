using Example.Notific.Context.Contract.CQRS.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NServiceBus;

namespace Example.Notific.Context.CQRS.Bus
{
    [Serializable]
    public class RetryNotificationByHttpPostBusCommand: RetryNotificationByHttpPostCommand, ICommand
    {
    }
}
