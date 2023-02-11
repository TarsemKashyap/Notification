using Example.Notific.Context.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NServiceBus;

namespace Example.Notific.Context.CQRS.Bus
{
    [Serializable]
    public class EventRecordedBusCommand : EventRecordedEvent, ICommand
    {

    }
}
