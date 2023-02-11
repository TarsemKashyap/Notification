using Example.Common.Context.CQRS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Example.Notific.Context.Events
{
    [Serializable]
    public class EventRecordedEvent : EventBase
    {
        public Guid EventId { get; set; }
    }
}
