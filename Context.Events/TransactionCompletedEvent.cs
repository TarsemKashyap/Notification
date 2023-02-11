using Example.Common.Context.CQRS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Example.MAP.PG.Context.Contract.Events.Events
{
    public class TransactionCompletedEvent : EventBase
    {
        public string TransactionNumber { get; set; }
    }
}
