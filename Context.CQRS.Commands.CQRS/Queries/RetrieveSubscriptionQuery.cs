using Example.Common.Context.CQRS;
using Example.Notific.Context.Contract.CQRS.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Example.Notific.Context.Contract.CQRS.Queries
{
    public class RetrieveSubscriptionQuery : IQuery<SubscriptionDto>
    {
        public long SubscriptionId { get; set; }
    }
}
