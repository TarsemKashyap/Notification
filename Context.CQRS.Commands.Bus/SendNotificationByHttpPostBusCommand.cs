using Example.Notific.Context.Contract.CQRS.Commands;
using NServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Example.Notific.Context.CQRS.Bus
{
    [Serializable]
    public class SendNotificationByHttpPostBusCommand : SendNotificationByHttpPostCommand, ICommand
    {

    }
}
