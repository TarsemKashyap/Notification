using Example.Common.Context.CQRS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Example.Notific.Context.Contract.CQRS.Commands
{
    public class TerminateSubscriptionCommand : CommandBase
    {
        public long SubscriptionId { get; set; }
    }
}
