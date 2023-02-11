using Example.Common.Context.CQRS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Example.Notific.Context.Events
{
    [Serializable]
    public class TPRecurringCardPlanCreatedEvent : EventBase
    {
        public int MerchantId { get; set; }
        public int PlanId { get; set; }
    }
}
