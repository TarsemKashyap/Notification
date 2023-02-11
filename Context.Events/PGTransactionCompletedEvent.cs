using Example.Common.Context.CQRS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Example.Notific.Context.Events
{
    [Serializable]
    public class PGTransactionCompletedEvent : EventBase
    {
        public string TransactionNumber { get; set; }
    }
}
