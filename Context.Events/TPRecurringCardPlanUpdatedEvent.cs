using Example.Common.Context.CQRS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Example.Notific.Context.Events
{
    public class TPRecurringCardPlanUpdatedEvent : EventBase
    {
        public int PlanId { get; set; }
    }
}
