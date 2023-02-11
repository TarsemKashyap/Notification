using Example.Common.Context.CQRS;
using Example.Notific.Context.Contract.CQRS.Dtos;
using System;

namespace Example.Notific.Context.Contract.CQRS.Queries
{
   public class RetrieveNotificationQuery : IQuery<string>
    {
        public Guid EventId { get; set; }

        public int MerchantId { get; set; }
    }
}
