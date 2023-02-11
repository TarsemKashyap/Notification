using Example.Common.Context.CQRS;
using Example.Notific.Context.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Example.Notific.Context.Contract.CQRS.Commands
{
    public class SubscribeToEventCommand: CommandBase
    {
        public int MerchantId { get; set; }

        public EventType EventType { get; set; }

        public DeliveryMethod DeliveryMethod { get; set; }

        public string DeliveryAddress { get; set; }

        public string Description { get; set; }

        public string Subscriber { get; set; }

        #region Output Values

        public long SubscriptionId { get; set; }

        #endregion

    }
}
