using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Example.Notific.Context.Contract.CQRS.Dtos
{
    public class SubscriptionDto
    {
        public long SubscriptionId { get; set; }

        public int MerchantId { get; set; }

        public int EventType { get; set; }

        public string DeliveryMethod { get; set; }

        public string DeliveryAddress { get; set; }

        public string Description { get; set; }

        public DateTime DateSubscribed { get; set; }

        public string Subscriber { get; set; }
    }
}
