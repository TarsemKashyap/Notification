using NServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Example.Notific.Context.Events.Bus
{
    [Serializable]
    public class EventRecordedBusEvent : EventRecordedEvent,IEvent
    {

    }
}
