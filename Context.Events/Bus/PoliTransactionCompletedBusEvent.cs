using Example.MAP.PG.Context.Contract.Events.Events;
using NServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Example.MAP.PG.Context.Contract.Events.Bus
{
    public class PoliTransactionCompletedBusEvent : PoliTransactionCompletedEvent, IEvent
    {

    }   
}
