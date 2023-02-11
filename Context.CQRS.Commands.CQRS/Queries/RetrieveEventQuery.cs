using Example.Common.Context.CQRS;
using Example.Notific.Context.Contract.CQRS.Dtos;
using System;

namespace Example.Notific.Context.Contract.CQRS.Queries
{
   public class RetrieveEventQuery : IQuery<EventDto>
    {
        public Guid EventId { get; set; }

        public int MerchantId { get; set; }
    }
}
